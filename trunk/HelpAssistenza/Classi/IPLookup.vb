Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Text.RegularExpressions

Public Class IPLookup

#Region " Public Property-Vars and properties "
    Private _IP As String
    Public Property IP() As String
        Get
            Return _IP
        End Get
        Set(ByVal value As String)
            _IP = value
            ParseAndLookup()
        End Set
    End Property

    Private _ISP As String
    Public ReadOnly Property ISP() As String
        Get
            Return _ISP
        End Get
    End Property

    Private _ISPDescription As String
    Public ReadOnly Property ISPDescription() As String
        Get
            Return _ISPDescription
        End Get
    End Property

    Private _Country As String
    Public ReadOnly Property Country() As String
        Get
            Return _Country
        End Get
    End Property

    Private _Service As String
    Public ReadOnly Property Service() As String
        Get
            Return _Service
        End Get
    End Property

    Private _ServiceText As String
    Public ReadOnly Property ServiceText() As String
        Get
            Return _ServiceText
        End Get
    End Property
#End Region

#Region " Private Class Variables "
    ' IP segments
    Private _IPSeg(4) As Integer

    ' Sites to do the lookup
    Private sites() As String = {"whois.apnic.net", _
                                 "whois.ripe.net", _
                                 "whois.arin.net"}
#End Region

    Public Sub New(ByVal thisIP As String)
        _IP = thisIP
        ParseAndLookup()
    End Sub

    Private Sub ParseAndLookup()
        ' Precondition: If IP is invalid, set error and exit
        If Not ParseIP() Then
            SetError()
            _ISP = "Broke at parse"
            Exit Sub
        End If

        ' Defined here to keep in scope later on
        Dim tempStore As String = String.Empty
        Dim strDomain As String = _IP & vbCrLf
        Dim arrDomain As Byte() = Encoding.ASCII.GetBytes(strDomain)

        Dim i As Integer ' dim here for scope
        For i = 0 To _IPSeg.Length
            ' Make a TCPClient and connect
            Dim tcpC As New TcpClient(sites(i), 43)
            Dim tcpStream As Stream = tcpC.GetStream

            ' Write the IP into the stream
            tcpStream.Write(arrDomain, 0, strDomain.Length)

            ' Read the return stream into a string
            Dim objSR As New StreamReader(tcpStream, Encoding.ASCII)
            tempStore = objSR.ReadToEnd

            ' Close the stream
            tcpC.Close()

            ' See if we have a winner yet
            Select Case i
                Case 0 ' Apnic
                    If Not (tempStore.Contains("RIPE") Or _
                            tempStore.Contains("arin")) Then
                        Exit For
                    End If
                Case 1 ' Ripe
                    If Not (tempStore.Contains("iana") Or _
                            tempStore.Contains("arin")) Then
                        Exit For
                    End If
                Case 2 ' Arin
                    If Not (tempStore.Contains("RIPE") Or _
                            tempStore.Contains("apnic")) Then
                        Exit For
                    End If
            End Select
        Next

        ' Store the service used into private var
        _Service = sites(i)

        ' Store the raw text
        _ServiceText = tempStore

        ' Get the data from the result
        Dim pat As String = "(?:netname:\s+(?<nname>.*)\s+" & _
                            "(?:descr:\s+(?<ndesc>.*)\s+)+" & _
                            "country:\s+(?<ncoun>.*)\s+)|" & _
                            "(?:OrgName:\s+(?<nname>.*)\s+" & _
                            "OrgID:\s+(?<ndesc>.*)\s+.*" & _
                            "country:\s+(?<ncoun>.*)\s+)"

        Dim match As Match = Regex.Match(tempStore, pat)

        ' Midcondition: explode if no match
        If Not match.Success Then
            SetError()
            _ISP = "Broke at regex"
            Exit Sub
        End If

        With match
            _ISP = .Groups("nname").Value
            _ISPDescription = .Groups("ndesc").Value
            _Country = .Groups("ncoun").Value
        End With

        ' Fin.

    End Sub


    ' Sub for churning the String-IP into Integer segments
    Private Function ParseIP() As Boolean

        ' Get a list of matches based on the given IP
        Dim _matches As MatchCollection = Regex.Matches(_IP, "(\d{1,3})")

        ' Midcondition: Check there are at least 4 maches
        If _matches.Count < 4 Then Return False

        ' Iterate the matches and assign
        Dim count As Integer = 0
        For Each _match As Match In _matches
            If count > 3 Then Exit For

            _IPSeg(count) = Integer.Parse(_match.Value)

            count += 1
        Next

        ' Postcondition: Check the segments to make sure they're under 256
        For Each seg As Integer In _IPSeg
            If seg > 255 Then Return False
        Next

        ' Reassign ip from segments
        _IP = String.Format("{0}.{1}.{2}.{3}", _
            _IPSeg(0), _IPSeg(1), _IPSeg(2), _IPSeg(3))

        Return True
    End Function


    Private Sub SetError()
        _ISP = "Error"
        _ISPDescription = "Invalid IP"
        _Country = "Unknown"
    End Sub

End Class
