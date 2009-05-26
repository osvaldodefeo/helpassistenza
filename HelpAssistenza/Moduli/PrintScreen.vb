Module PrintScreen
    Private Declare Function CreateDC Lib "gdi32" Alias "CreateDCA" (ByVal lpDriverName As String, ByVal lpDeviceName As String, ByVal lpOutput As String, ByVal lpInitData As String) As Integer
    Private Declare Function CreateCompatibleDC Lib "GDI32" (ByVal hDC As Integer) As Integer
    Private Declare Function CreateCompatibleBitmap Lib "GDI32" (ByVal hDC As Integer, ByVal nWidth As Integer, ByVal nHeight As Integer) As Integer
    Private Declare Function GetDeviceCaps Lib "gdi32" Alias "GetDeviceCaps" (ByVal hdc As Integer, ByVal nIndex As Integer) As Integer
    Private Declare Function SelectObject Lib "GDI32" (ByVal hDC As Integer, ByVal hObject As Integer) As Integer
    Private Declare Function BitBlt Lib "GDI32" (ByVal srchDC As Integer, ByVal srcX As Integer, ByVal srcY As Integer, ByVal srcW As Integer, ByVal srcH As Integer, ByVal desthDC As Integer, ByVal destX As Integer, ByVal destY As Integer, ByVal op As Integer) As Integer
    Private Declare Function DeleteDC Lib "GDI32" (ByVal hDC As Integer) As Integer
    Private Declare Function DeleteObject Lib "GDI32" (ByVal hObj As Integer) As Integer
    Const SRCCOPY As Integer = &HCC0020
    Public Schermata As Bitmap
    Private fw, fh As Integer

    Public Sub CaptureScreen()
        Dim hsdc, hmdc As Integer
        Dim hbmp, hbmpold As Integer
        Dim r As Integer
        hsdc = CreateDC("DISPLAY", "", "", "")
        hmdc = CreateCompatibleDC(hsdc)
        fw = GetDeviceCaps(hsdc, 8)
        fh = GetDeviceCaps(hsdc, 10)
        hbmp = CreateCompatibleBitmap(hsdc, fw, fh)
        hbmpold = SelectObject(hmdc, hbmp)
        r = BitBlt(hmdc, 0, 0, fw, fh, hsdc, 0, 0, 13369376)
        hbmp = SelectObject(hmdc, hbmpold)
        r = DeleteDC(hsdc)
        r = DeleteDC(hmdc)
        Schermata = Image.FromHbitmap(New IntPtr(hbmp))
        DeleteObject(hbmp)
    End Sub
End Module
