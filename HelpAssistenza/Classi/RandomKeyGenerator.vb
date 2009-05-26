Option Strict On
Imports System.Text
Imports System.Threading
Public Class RandomKeyGenerator
    Dim Key_Letters As String
    Dim Key_Numbers As String
    Dim Key_Chars As Integer
    Dim WType As String
    Dim LettersArray As Char()
    Dim NumbersArray As Char()

    Dim i_key As Integer
    Dim Random1 As Single
    Dim Random2 As Single
    Dim Random3 As Single
    Dim Num As Single
    Dim arrIndex As Int16
    Dim sb As New StringBuilder()
    Dim RandomLetter As String

    Protected Friend WriteOnly Property KeyLetters() As String
        Set(ByVal Value As String)
            Key_Letters = Value
        End Set
    End Property

    Protected Friend WriteOnly Property KeyNumbers() As String
        Set(ByVal Value As String)
            Key_Numbers = Value
        End Set
    End Property

    Protected Friend WriteOnly Property KeyChars() As Integer
        Set(ByVal Value As Integer)
            Key_Chars = Value
        End Set
    End Property

    Protected Friend WriteOnly Property Type() As String
        Set(ByVal Value As String)
            WType = Value
        End Set
    End Property

    Function Generate() As String

        If WType = "L" Then
            LettersArray = Key_Letters.ToCharArray
        Else
            NumbersArray = Key_Numbers.ToCharArray
        End If

        For Me.i_key = 1 To Key_Chars

            Randomize()
            Random1 = Rnd()
            Thread.Sleep(10)
            Randomize()
            Random2 = Rnd()
            arrIndex = -1

            If WType = "L" Then
                Do While arrIndex < 0
                    OddEven()
                    If Num Mod 2 = 1 Then
                        arrIndex = Convert.ToInt16(LettersArray.GetUpperBound(0) * Random1)
                    Else
                        arrIndex = Convert.ToInt16(LettersArray.GetUpperBound(0) * Random2)
                    End If
                Loop
                RandomLetter = LettersArray(arrIndex)
                sb.Append(RandomLetter)
            Else
                Do While arrIndex < 0
                    OddEven()
                    If Num Mod 2 = 1 Then
                        arrIndex = Convert.ToInt16(NumbersArray.GetUpperBound(0) * Random1)
                    Else
                        arrIndex = Convert.ToInt16(NumbersArray.GetUpperBound(0) * Random2)
                    End If
                Loop
                sb.Append(NumbersArray(arrIndex))
            End If
        Next
        Return sb.ToString
    End Function

    Private Sub OddEven()
        Randomize()
        Random3 = Rnd()
        Num = Random3 / 2
    End Sub
End Class
