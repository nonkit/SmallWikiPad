Module SmallWikiPadModule
    Dim CRLF, LT, tbox, wikiText, dx, mx, mouseDown, tabClicked, i, lastTabClicked, iLast, hline, grp, group, iDocument, iMin, iMax, iScrollBar, iThumb, htmlText, nGroup, scale, angle, shX, shY, ph, shHeight, ymaxPic, yminPic, gh, x, y, gw, height, iTabs, n, _i, shp, shape, menu, posX, item, dy, my, mouseMove, nShape, th, yminThumb, ymaxThumb, hPage, yMove, x1, x2, y1, y2, y3, y4, y5, y6, yLast, myLast, myThumb, r, a, ox, oy, alpha, color, silverlight, msWait, s, fs, shAngle, _cx, param, _cy, xmin, xmax, ymin, ymax, shWidth, _x, _y, cx, cy As Primitive
    Sub Main()
        ' Small Wiki Pad
        ' Version 0.31a
        ' Copyright © 2015 Nonki Takahashi.  The MIT License.
        ' Last update 2015-12-30
        ' Program ID NVD371-4
        '
        GraphicsWindow.Title = "Small Wiki Pad 0.31a"
        CRLF = Text.GetCharacter(13) + Text.GetCharacter(10)
        LT = "<"
        SB_Workaround()
        Form()
        LoadWikiText()
        LoadHTMLText()
        Controls.SetTextBoxText(tbox, wikiText)
        AddHandler GraphicsWindow.MouseDown, AddressOf OnMouseDown
        AddHandler GraphicsWindow.MouseMove, AddressOf OnMouseMove
        While true
            If mouseDown Then
                Controls_TabClick()
                If tabClicked Then
                    ChangeTab()
                Else
                    Controls_ScrollBar()
                End If
                mouseDown = false
            End If
        End While
    End Sub
    Sub ChangeTab()
        i = lastTabClicked
        If i <> iLast Then
            Shapes.ShowShape(hline(iLast))
            Shapes.HideShape(hline(i))
            If (i = 1) Or (i = 2) Then
                If iLast = 3 Then
                    grp = group(iDocument)
                    iMin = grp("iMin")
                    iMax = grp("iMax")
                    Shapes_Remove()
                    grp("y") = 10
                    group(iDocument) = grp
                    grp = group(iScrollBar)
                    iMin = grp("iMin")
                    iMax = grp("iMax")
                    Shapes_Remove()
                    grp = group(iThumb)
                    iMin = grp("iMin")
                    iMax = grp("iMax")
                    Shapes_Remove()
                    grp("y") = 32 + 12
                    group(iThumb) = grp
                End If
                Controls.ShowControl(tbox)
                If i = 1 Then
                    Controls.SetTextBoxText(tbox, wikiText)
                ElseIf i = 2 Then
                    Controls.SetTextBoxText(tbox, htmlText)
                End If
            ElseIf i = 3 Then
                Controls.HideControl(tbox)
                ' initialize shapes
                If iDocument = CType("", Primitive) Then
                    Shapes_Init()
                    iDocument = nGroup
                End If
                ' add shapes
                scale = 1
                angle = 0
                grp = group(iDocument)
                shX = grp("x")
                shY = grp("y")
                iMin = grp("iMin")
                iMax = grp("iMax")
                Shapes_Add()
                ph = shHeight ' picture height
                ymaxPic = 32 + shY
                yminPic = gh - ph
                x = shX
                y = ymaxPic
                Shapes_Move()
                If iScrollBar = CType("", Primitive) Then
                    x = gw - 12
                    y = 32
                    height = gh - y
                    scale = 1
                    Controls_AddVScroll()
                Else
                    grp = group(iScrollBar)
                    shX = grp("x")
                    shY = grp("y")
                    iMin = grp("iMin")
                    iMax = grp("iMax")
                    Shapes_Add()
                    grp = group(iThumb)
                    shX = grp("x")
                    shY = grp("y")
                    iMin = grp("iMin")
                    iMax = grp("iMax")
                    Shapes_Add()
                End If
                grp = group(iTabs)
                shX = grp("x")
                shY = grp("y")
                iMin = grp("iMin")
                iMax = grp("iMax")
                Shapes_Remove()
                Shapes_Add()
                n = iMin - 1
                For _i = 1 To 3
                    shp = shape(n + (_i * 3))
                    menu(_i) = shp("obj")
                    shp = shape(n + (_i * 3) + 2)
                    hline(_i) = shp("obj")
                Next
                Shapes.HideShape(hline(i))
            End If
            iLast = i
        End If
    End Sub
    Sub Form()
        gw = 598
        gh = 428
        GraphicsWindow.Width = gw
        GraphicsWindow.Height = gh
        GraphicsWindow.BrushColor = "#333333"
        GraphicsWindow.FontName = "Segoe UI"
        GraphicsWindow.FontBold = false
        GraphicsWindow.PenWidth = 1
        GraphicsWindow.PenColor = "#666666"
        posX = "1=5;2=105;3=205;4=305;"
        item = "1=Wiki;2=HTML;3=Preview;"
        scale = 1
        Controls_AddTabs()
        iTabs = nGroup
        tbox = Controls.AddMultiLineTextBox(posX(1), 35)
        Controls.SetSize(tbox, gw - 10, gh - 40)
    End Sub
    Sub OnMouseDown()
        dx = GraphicsWindow.MouseX
        dy = GraphicsWindow.MouseY
        mouseDown = true
    End Sub
    Sub OnMouseMove()
        mx = GraphicsWindow.MouseX
        my = GraphicsWindow.MouseY
        mouseMove = true
    End Sub
    Sub LoadWikiText()
        wikiText = "*bold*" + CRLF
        wikiText = wikiText + "_italics_" + CRLF
        wikiText = wikiText + "+underline+" + CRLF
        wikiText = wikiText + "! Heading 1" + CRLF
        wikiText = wikiText + "!! Heading 2" + CRLF
        wikiText = wikiText + "!!! Heading 3" + CRLF
        wikiText = wikiText + "* Bullet List" + CRLF
        wikiText = wikiText + "** Bullet List 2" + CRLF
        wikiText = wikiText + "# Number List" + CRLF
        wikiText = wikiText + "## Number List 2" + CRLF
        wikiText = wikiText + "[image:Turtle.png]" + CRLF
        wikiText = wikiText + "||Table Heading 1||Table Heading 2||" + CRLF
        wikiText = wikiText + "|Row 1 - Cell 1|Row 1 - Cell 2|" + CRLF
        wikiText = wikiText + "|Row 2 - Cell 1|Row 2 - Cell 2|" + CRLF
        wikiText = wikiText + "----"
    End Sub
    Sub LoadHTMLText()
        htmlText = LT + "html>" + CRLF
        htmlText = htmlText + LT + "body>" + CRLF
        htmlText = htmlText + LT + "p>" + CRLF
        htmlText = htmlText + LT + "strong>bold" + LT + "/strong>" + LT + "br>" + CRLF
        htmlText = htmlText + LT + "em>italics" + LT + "/em>" + LT + "br>" + CRLF
        htmlText = htmlText + LT + "span style='text-decoration:underline;'>underline" + LT + "/span>" + CRLF
        htmlText = htmlText + LT + "/p>" + CRLF
        htmlText = htmlText + LT + "h1>Heading 1" + LT + "/h1>" + CRLF
        htmlText = htmlText + LT + "h2>Heading 2" + LT + "/h2>" + CRLF
        htmlText = htmlText + LT + "h3>Heading 3" + LT + "/h3>" + CRLF
        htmlText = htmlText + LT + "ul>" + CRLF
        htmlText = htmlText + LT + "li>Bullet List" + LT + "/li>" + LT + "br>" + CRLF
        htmlText = htmlText + LT + "ul>" + CRLF
        htmlText = htmlText + LT + "li>Bullet List 2" + LT + "/li>" + CRLF
        htmlText = htmlText + LT + "/ul>" + CRLF
        htmlText = htmlText + LT + "/ul>" + CRLF
        htmlText = htmlText + LT + "ol>" + CRLF
        htmlText = htmlText + LT + "li>Number List" + LT + "/li>" + LT + "br>" + CRLF
        htmlText = htmlText + LT + "ol>" + CRLF
        htmlText = htmlText + LT + "li>Number List 2" + LT + "/li>" + CRLF
        htmlText = htmlText + LT + "/ol>" + CRLF
        htmlText = htmlText + LT + "/ol>" + CRLF
        htmlText = htmlText + LT + "p>" + LT + "img src='http://www.nonkit.com/smallbasic.files/Turtle.png'>" + LT + "/p>" + CRLF
        htmlText = htmlText + LT + "table border=1>" + CRLF
        htmlText = htmlText + LT + "tr>" + LT + "th>Table Heading 1" + LT + "/th>" + LT + "th>Table Heading 2" + LT + "/th>" + LT + "/tr>" + CRLF
        htmlText = htmlText + LT + "tr>" + LT + "td>Row 1-Cell 1" + LT + "/td>" + LT + "td>Row 1-Cell 2" + LT + "/td>" + LT + "/tr>" + CRLF
        htmlText = htmlText + LT + "tr>" + LT + "td>Row 2-Cell 1" + LT + "/td>" + LT + "td>Row 2-Cell 2" + LT + "/td>" + LT + "/tr>" + CRLF
        htmlText = htmlText + LT + "/table>" + CRLF
        htmlText = htmlText + LT + "hr>" + CRLF
        htmlText = htmlText + LT + "/body>" + CRLF
        htmlText = htmlText + LT + "/html>"
    End Sub
    Sub Shapes_Init()
        ' Shapes | Initialize shapes data
        ' return shX, shY - current position of shapes
        ' return shape - array of shapes
        shX = 10 ' x offset
        shY = 10 ' y offset
        grp = ""
        grp("x") = shX
        grp("y") = shY
        n = nShape
        grp("iMin") = n + 1
        shape(n + 1) = "func=text;x=0;y=0;fn=Segoe UI;fs=12;fb=True;text=bold;bc=#000000;"
        shape(n + 2) = "func=text;x=0;y=16;fn=Segoe UI;fs=12;fi=True;text=italics;bc=#000000;"
        shape(n + 3) = "func=text;x=0;y=32;fn=Segoe UI;fs=12;text=underline;bc=#000000;"
        shape(n + 4) = "func=rect;x=0;y=46;width=50;height=1;pc=#000000;pw=1;"
        shape(n + 5) = "func=text;x=0;y=58;fn=Segoe UI;fs=36;fb=True;text=Heading 1;bc=#000000;"
        shape(n + 6) = "func=text;x=0;y=98;fn=Segoe UI;fs=26;fb=True;text=Heading 2;bc=#000000;"
        shape(n + 7) = "func=text;x=0;y=128;fn=Segoe UI;fs=16;fb=True;text=Heading 3;bc=#000000;"
        shape(n + 8) = "func=ell;x=20;y=154;x2=50;y2=54;width=5;height=5;bc=#000000;pw=0;"
        shape(n + 9) = "func=text;x=30;y=148;fn=Segoe UI;fs=12;text=Bullet List;bc=#000000;"
        shape(n + 10) = "func=ell;x=40;y=170;x2=50;y2=54;width=5;height=5;bc=#FFFFFF;pc=#000000;pw=1;"
        shape(n + 11) = "func=text;x=50;y=164;fn=Segoe UI;fs=12;text=Bullet List 2;bc=#000000;"
        shape(n + 12) = "func=text;x=18;y=180;fn=Segoe UI;fs=12;text=1. Number List;bc=#000000;"
        shape(n + 13) = "func=text;x=38;y=196;fn=Segoe UI;fs=12;text=1. Number List 2;bc=#000000;"
        shape(n + 14) = "func=img;x=0;y=212;src=http://www.nonkit.com/smallbasic.files/Turtle.png;"
        shape(n + 15) = "func=rect;x=0;y=464;width=200;height=54;bc=#FFFFFF;pc=#666666;pw=1;"
        shape(n + 16) = "func=rect;x=0;y=482;width=200;height=1;pc=#666666;pw=1;"
        shape(n + 17) = "func=rect;x=0;y=500;width=200;height=1;pc=#666666;pw=1;"
        shape(n + 18) = "func=rect;x=100;y=464;width=1;height=54;pc=#666666;pw=1;"
        shape(n + 19) = "func=text;x=2;y=464;fn=Segoe UI;fs=12;fb=True;text=Table Heading 1;bc=#000000;"
        shape(n + 20) = "func=text;x=102;y=464;fn=Segoe UI;fs=12;fb=True;text=Table Heading 2;bc=#000000;"
        shape(n + 21) = "func=text;x=2;y=482;fn=Segoe UI;fs=12;text=Row 1-Cell 1;bc=#000000;"
        shape(n + 22) = "func=text;x=102;y=482;fn=Segoe UI;fs=12;text=Row 1-Cell 2;bc=#000000;"
        shape(n + 23) = "func=text;x=2;y=500;fn=Segoe UI;fs=12;text=Row 2-Cell 1;bc=#000000;"
        shape(n + 24) = "func=text;x=102;y=500;fn=Segoe UI;fs=12;text=Row 2-Cell 2;bc=#000000;"
        shape(n + 25) = "func=rect;x=0;y=522;width=578;height=1;pc=#666666;pw=1;"
        nShape = nShape + 25
        grp("iMax") = nShape
        nGroup = nGroup + 1 ' number of group
        group(nGroup) = grp
    End Sub
    Sub Controls_AddTabs()
        shX = 0 ' x offset
        shY = 0 ' y offset
        grp = ""
        grp("x") = shX
        grp("y") = shY
        n = nShape
        grp("iMin") = n + 1
        shape(n + 1) = "func=rect;x=0;y=0;width=" + gw + ";height=32;pw=0;bc=#FFFFFF;"
        shape(n + 2) = "func=line;x=" + posX(1) + ";y=5;x1=0;y1=0;x2=" + (posX(4) - posX(1)) + ";y2=0;pw=1;pc=#666666;"
        For i = 1 To 3
            shape(n + (i * 3)) = "func=text;x=" + (posX(i) + 5) + ";y=10;text=" + item(i) + ";fn=Segoe UI;fs=12;"
            shape(n + (i * 3) + 1) = "func=line;x=" + posX(i) + ";y=5;x1=0;y1=0;x2=0;y2=25;pw=1;pc=#666666;"
            shape(n + (i * 3) + 2) = "func=line;x=" + posX(i) + ";y=30;x1=0;y1=0;x2=" + (posX(i + 1) - posX(i)) + ";y2=0;pw=1;pc=#666666;"
        Next
        iLast = 1
        shape(n + 12) = "func=line;x=" + posX(4) + ";y=5;x1=0;y1=0;x2=0;y2=25;pw=1;pc=#666666;"
        shape(n + 13) = "func=line;x=" + posX(4) + ";y=30;x1=0;y1=0;x2=" + (gw - 5 - posX(4)) + ";y2=0;pw=1;pc=#666666;"
        nShape = nShape + 13
        grp("iMax") = nShape
        nGroup = nGroup + 1 ' number of group
        group(nGroup) = grp
        iMin = grp("iMin")
        iMax = grp("iMax")
        Shapes_Add()
        For i = 1 To 3
            shp = shape(n + (i * 3))
            menu(i) = shp("obj")
            shp = shape(n + (i * 3) + 2)
            hline(i) = shp("obj")
        Next
        Shapes.HideShape(hline(iLast))
    End Sub
    Sub Controls_AddVScroll()
        ' param x - left position x
        ' param y - top position y
        ' param height - height
        ' return group - array of shapes groups
        ' return nShape - number of indices for shape array
        ' return nGroup - number of indices for group array
        ' add vertical scroll bar
        shX = x
        shY = y
        grp = ""
        grp("x") = shX
        grp("y") = shY
        n = nShape
        grp("iMin") = n + 1
        shape(n + 1) = "func=rect;x=0;y=0;width=12;height=" + height + ";pw=0;bc=#99CCCCCC;"
        shape(n + 2) = "func=rect;x=0;y=0;width=12;height=12;pw=0;bc=#CCCCCC;"
        shape(n + 3) = "func=line;x=3;y=4;x1=0;y1=3;x2=3;y2=0;pw=1;pc=#666666;"
        shape(n + 4) = "func=line;x=6;y=4;x1=0;y1=0;x2=3;y2=3;pw=1;pc=#666666;"
        shape(n + 5) = "func=rect;x=0;y=" + (height - 12) + ";width=12;height=12;pw=0;bc=#CCCCCC;"
        shape(n + 6) = "func=line;x=3;y=" + (height - 8) + ";x1=0;y1=0;x2=3;y2=3;pw=1;pc=#666666;"
        shape(n + 7) = "func=line;x=6;y=" + (height - 8) + ";x1=0;y1=3;x2=3;y2=0;pw=1;pc=#666666;"
        nShape = nShape + 7
        grp("iMax") = nShape
        nGroup = nGroup + 1 ' number of group
        group(nGroup) = grp
        iScrollBar = nGroup
        iMin = grp("iMin")
        iMax = grp("iMax")
        scale = 1
        Shapes_Add()
        ' add thumb
        th = (height - 24) * height / ph ' thumb heignt
        shX = x
        shY = y + 12
        grp = ""
        grp("x") = shX
        grp("y") = shY
        n = nShape
        grp("iMin") = n + 1
        shape(n + 1) = "func=rect;x=0;y=0;width=12;height=" + th + ";pw=0;bc=#999999;"
        nShape = nShape + 1
        grp("iMax") = nShape
        nGroup = nGroup + 1 ' number of group
        group(nGroup) = grp
        iThumb = nGroup
        iMin = grp("iMin")
        iMax = grp("iMax")
        Shapes_Add()
        yminThumb = y + 12
        ymaxThumb = y + height - 12 - th
        hPage = (ph - ymaxPic) / ph * (ymaxThumb - yminThumb) ' page height in scroll bar
    End Sub
    Sub Controls_Scroll()
        ' param yMove - relative thumb move
        grp = group(iDocument)
        x = grp("x")
        y = grp("y") - (yMove * gh / (ymaxThumb - yminThumb))
        iMin = grp("iMin")
        iMax = grp("iMax")
        Shapes_Move()
        grp("y") = y
        group(iDocument) = grp
    End Sub
    Sub Controls_ScrollBar()
        grp = group(iScrollBar)
        iMin = grp("iMin")
        shX = grp("x")
        shY = grp("y")
        shp = shape(iMin) ' vertical scroll bar
        x1 = shX + shp("rx")
        x2 = shX + shp("rx") + shp("width")
        y1 = shY + shp("ry")
        y2 = shY + shp("ry") + shp("height")
        If (x1 <= dx) And (dx <= x2) And (y1 <= dy) And (dy <= y2) Then
            shp = shape(iMin + 1) ' line up button
            y1 = shY + shp("ry")
            y2 = shY + shp("ry") + shp("height")
            shp = shape(iMin + 4) ' line down button
            y3 = shY + shp("ry")
            y4 = shY + shp("ry") + shp("height")
            grp = group(iThumb) ' thumb
            iMin = grp("iMin")
            iMax = grp("iMax")
            shX = grp("x")
            shY = grp("y")
            shp = shape(iMin)
            y5 = shY + shp("ry")
            y6 = shY + shp("ry") + shp("height")
            y = y5
            yLast = y5
            If (y1 <= dy) And (dy <= y2) Then
                ' line up
                x = shX
                y = y5 - (hPage / 25)
                If y < yminThumb Then
                    y = yminThumb
                End If
            ElseIf (y3 <= dy) And (dy <= y4) Then
                ' line down
                x = shX
                y = y5 + (hPage / 25)
                If ymaxThumb < y Then
                    y = ymaxThumb
                End If
            ElseIf (y5 <= dy) And (dy <= y6) Then
                ' thumb truck
                myLast = dy
                myThumb = dy - y5
                While Mouse.IsLeftButtonDown
                    If mouseMove Then
                        x = shX
                        y = my - myThumb
                        If ymaxThumb < y Then
                            y = ymaxThumb
                        ElseIf y < yminThumb Then
                            y = yminThumb
                        End If
                        If y <> yLast Then
                            yMove = y - yLast
                            yLast = y
                            Shapes_Move() ' move thumb
                            grp("y") = shY
                            group(iThumb) = grp ' thumb
                            Controls_Scroll()
                        End If
                        myLast = my
                        mouseMove = false
                    End If
                End While
            ElseIf dy < y5 Then
                ' page up
                x = shX
                y = y5 - hPage
                If y < yminThumb Then
                    y = yminThumb
                End If
            ElseIf y6 < dy Then
                ' page down
                x = shX
                y = y5 + hPage
                If ymaxThumb < y Then
                    y = ymaxThumb
                End If
            End If
            If y <> yLast Then
                yMove = y - yLast
                Shapes_Move() ' move thumb
                grp("y") = shY
                group(iThumb) = grp ' thumb
                Controls_Scroll()
            End If
        End If
    End Sub
    Sub Controls_TabClick()
        tabClicked = false
        If (5 <= dy) And (dy < 30) Then
            For i = 1 To 3
                If (posX(i) <= dx) And (dx < posX(i + 1)) Then
                    tabClicked = true
                    lastTabClicked = i
                    i = 3 ' exit for
                End If
            Next
        End If
    End Sub
    Sub Math_CartesianToPolar()
        ' Math | convert cartesian coodinate to polar coordinate
        ' param x, y - cartesian coordinate
        ' return r, a - polar coordinate
        r = Microsoft.SmallBasic.Library.Math.SquareRoot((x * x) + (y * y))
        If (x = 0) And (y > 0) Then
            a = 90 ' [degree]
        ElseIf (x = 0) And (y < 0) Then
            a = -90
        Else
            a = Microsoft.SmallBasic.Library.Math.ArcTan(y / x) * 180 / Microsoft.SmallBasic.Library.Math.Pi
        End If
        If x < 0 Then
            a = a + 180
        ElseIf (x > 0) And (y < 0) Then
            a = a + 360
        End If
    End Sub
    Sub SB_RotateWorkaround()
        ' Small Basic | Rotate workaround for Silverlight
        ' param shp - current shape
        ' param x, y - original coordinate
        ' param alpha - angle [radian]
        ' returns x, y - workaround coordinate
        If shp("func") = CType("tri", Primitive) Then
            x1 = -Microsoft.SmallBasic.Library.Math.Floor(shp("x3") / 2)
            y1 = -Microsoft.SmallBasic.Library.Math.Floor(shp("y3") / 2)
        ElseIf shp("func") = CType("line", Primitive) Then
            x1 = -Microsoft.SmallBasic.Library.Math.Floor(Microsoft.SmallBasic.Library.Math.Abs(shp("x1") - shp("x2")) / 2)
            y1 = -Microsoft.SmallBasic.Library.Math.Floor(Microsoft.SmallBasic.Library.Math.Abs(shp("y1") - shp("y2")) / 2)
        End If
        ox = x - x1
        oy = y - y1
        x = (x1 * Microsoft.SmallBasic.Library.Math.Cos(alpha)) - (y1 * Microsoft.SmallBasic.Library.Math.Sin(alpha)) + ox
        y = (x1 * Microsoft.SmallBasic.Library.Math.Sin(alpha)) + (y1 * Microsoft.SmallBasic.Library.Math.Cos(alpha)) + oy
    End Sub
    Sub SB_Workaround()
        ' Small Basic | Workaround for Silverlight
        ' returns silverlight - "True" if in remote
        color = GraphicsWindow.GetPixel(0, 0)
        If Text.GetLength(color) > 7 Then
            silverlight = true
            msWait = 300
        Else
            silverlight = false
        End If
    End Sub
    Sub Shapes_Add()
        ' Shapes | add shapes as shapes data
        ' param iMin, iMax - shape indices to add
        ' param shape - array of shapes
        ' param scale - 1 if same scale
        ' return shWidth, shHeight - total size of shapes
        ' return shAngle - current angle of shapes
        Stack.PushValue("local", i)
        Stack.PushValue("local", x)
        Stack.PushValue("local", y)
        Shapes_CalcWidthAndHeight()
        s = scale
        For i = iMin To iMax
            shp = shape(i)
            GraphicsWindow.PenWidth = shp("pw") * s
            If shp("pw") > 0 Then
                GraphicsWindow.PenColor = shp("pc")
            End If
            If Text.IsSubText("rect|ell|tri|text", shp("func")) Then
                GraphicsWindow.BrushColor = shp("bc")
            End If
            If shp("func") = CType("rect", Primitive) Then
                shp("obj") = Shapes.AddRectangle(shp("width") * s, shp("height") * s)
            ElseIf shp("func") = CType("ell", Primitive) Then
                shp("obj") = Shapes.AddEllipse(shp("width") * s, shp("height") * s)
            ElseIf shp("func") = CType("tri", Primitive) Then
                shp("obj") = Shapes.AddTriangle(shp("x1") * s, shp("y1") * s, shp("x2") * s, shp("y2") * s, shp("x3") * s, shp("y3") * s)
            ElseIf shp("func") = CType("line", Primitive) Then
                shp("obj") = Shapes.AddLine(shp("x1") * s, shp("y1") * s, shp("x2") * s, shp("y2") * s)
            ElseIf shp("func") = CType("text", Primitive) Then
                fs = shp("fs")
                GraphicsWindow.FontSize = fs * s
                GraphicsWindow.FontName = shp("fn")
                GraphicsWindow.FontBold = shp("fb")
                GraphicsWindow.FontItalic = shp("fi")
                shp("obj") = Shapes.AddText(shp("text"))
            ElseIf shp("func") = CType("img", Primitive) Then
                shp("obj") = Shapes.AddImage(shp("src"))
                Shapes.Move(shp("obj"), shp("x"), shp("y"))
            End If
            x = shp("x")
            y = shp("y")
            shp("rx") = x
            shp("ry") = y
            If silverlight And Text.IsSubText("tri|line", shp("func")) Then
                alpha = Microsoft.SmallBasic.Library.Math.GetRadians(shp("angle"))
                SB_RotateWorkaround()
                shp("wx") = x
                shp("wy") = y
            End If
            Shapes.Move(shp("obj"), shX + (x * s), shY + (y * s))
            If Text.IsSubText("rect|ell|tri|text", shp("func")) And (shp("angle") <> 0) And (shp("angle") <> CType("", Primitive)) Then
                Shapes.Rotate(shp("obj"), shp("angle"))
            End If
            shape(i) = shp
        Next
        shAngle = 0
        y = Stack.PopValue("local")
        x = Stack.PopValue("local")
        i = Stack.PopValue("local")
    End Sub
    Sub Shapes_CalcRotatePos()
        ' Shapes | Calculate position for rotated shape
        ' param["x"], param["y"] - position of a shape
        ' param["width"], param["height"] - size of a shape
        ' param ["cx"], param["cy"] - center of rotation
        ' param ["angle"] - rotate angle
        ' return x, y - rotated position of a shape
        _cx = param("x") + (param("width") / 2)
        _cy = param("y") + (param("height") / 2)
        x = _cx - param("cx")
        y = _cy - param("cy")
        Math_CartesianToPolar()
        a = a + param("angle")
        x = r * Microsoft.SmallBasic.Library.Math.Cos(a * Microsoft.SmallBasic.Library.Math.Pi / 180)
        y = r * Microsoft.SmallBasic.Library.Math.Sin(a * Microsoft.SmallBasic.Library.Math.Pi / 180)
        _cx = x + param("cx")
        _cy = y + param("cy")
        x = _cx - (param("width") / 2)
        y = _cy - (param("height") / 2)
    End Sub
    Sub Shapes_CalcWidthAndHeight()
        ' Shapes | Calculate total width and height of shapes
        ' param iMin, iMax - shape indices to add
        ' return shWidth, shHeight - total size of shapes
        For i = iMin To iMax
            shp = shape(i)
            If (shp("func") = CType("tri", Primitive)) Or (shp("func") = CType("line", Primitive)) Then
                xmin = shp("x1")
                xmax = shp("x1")
                ymin = shp("y1")
                ymax = shp("y1")
                If shp("x2") < xmin Then
                    xmin = shp("x2")
                End If
                If xmax < shp("x2") Then
                    xmax = shp("x2")
                End If
                If shp("y2") < ymin Then
                    ymin = shp("y2")
                End If
                If ymax < shp("y2") Then
                    ymax = shp("y2")
                End If
                If shp("func") = CType("tri", Primitive) Then
                    If shp("x3") < xmin Then
                        xmin = shp("x3")
                    End If
                    If xmax < shp("x3") Then
                        xmax = shp("x3")
                    End If
                    If shp("y3") < ymin Then
                        ymin = shp("y3")
                    End If
                    If ymax < shp("y3") Then
                        ymax = shp("y3")
                    End If
                End If
                shp("width") = xmax - xmin
                shp("height") = ymax - ymin
            End If
            If i = 1 Then
                shWidth = shp("x") + shp("width")
                shHeight = shp("y") + shp("height")
            Else
                If shWidth < (shp("x") + shp("width")) Then
                    shWidth = shp("x") + shp("width")
                End If
                If shHeight < (shp("y") + shp("height")) Then
                    shHeight = shp("y") + shp("height")
                End If
            End If
            shape(i) = shp
        Next
    End Sub
    Sub Shapes_Dump()
        For i = 1 To Microsoft.SmallBasic.Library.Array.GetItemCount(shape)
            TextWindow.WriteLine(shape(i))
        Next
    End Sub
    Sub Shapes_Move()
        ' Shapes | Move shapes
        ' param iMin, iMax - shape indices to add
        ' param shape - array of shapes
        ' param scale - to zoom
        ' param x, y - position to move
        ' return shX, shY - new position of shapes
        Stack.PushValue("local", i)
        s = scale
        shX = x
        shY = y
        For i = iMin To iMax
            shp = shape(i)
            If silverlight And Text.IsSubText("tri|line", shp("func")) Then
                _x = shp("wx")
                _y = shp("wy")
            Else
                _x = shp("rx")
                _y = shp("ry")
            End If
            Shapes.Move(shp("obj"), shX + (_x * s), shY + (_y * s))
        Next
        i = Stack.PopValue("local")
    End Sub
    Sub Shapes_Remove()
        ' Shapes | Remove shapes
        ' param iMin, iMax - shapes indices to remove
        ' param shape - array of shapes
        Stack.PushValue("local", i)
        For i = iMin To iMax
            shp = shape(i)
            Shapes.Remove(shp("obj"))
        Next
        i = Stack.PopValue("local")
    End Sub
    Sub Shapes_Rotate()
        ' Shapes | Rotate shapes
        ' param iMin, iMax - shapes indices to rotate
        ' param shape - array of shapes
        ' param cx, cy - rotation center
        ' param scale - to zoom
        ' param angle - to rotate
        Stack.PushValue("local", i)
        Stack.PushValue("local", x)
        Stack.PushValue("local", y)
        s = scale
        param("angle") = angle
        If cx <> CType("", Primitive) Then
            param("cx") = cx
        Else
            cx = "" ' to avoid syntax error
            param("cx") = shWidth / 2
        End If
        If cy <> CType("", Primitive) Then
            param("cy") = cy
        Else
            cy = "" ' to avoid syntax error
            param("cy") = shHeight / 2
        End If
        For i = iMin To iMax
            shp = shape(i)
            param("x") = shp("x")
            param("y") = shp("y")
            param("width") = shp("width")
            param("height") = shp("height")
            Shapes_CalcRotatePos()
            shp("rx") = x
            shp("ry") = y
            If silverlight And Text.IsSubText("tri|line", shp("func")) Then
                alpha = Microsoft.SmallBasic.Library.Math.GetRadians(angle + shp("angle"))
                SB_RotateWorkAround()
                shp("wx") = x
                shp("wy") = y
            End If
            Shapes.Move(shp("obj"), shX + (x * s), shY + (y * s))
            Shapes.Rotate(shp("obj"), angle + shp("angle"))
            shape(i) = shp
        Next
        y = Stack.PopValue("local")
        x = Stack.PopValue("local")
        i = Stack.PopValue("local")
    End Sub
End Module
