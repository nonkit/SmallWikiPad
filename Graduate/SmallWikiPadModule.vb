Module SmallWikiPadModule
    Dim CR, LF, LT, WQ, tbox, wikiText, dx, mx, mouseDown, tabClicked, i, lastTabClicked, iLast, hline, iDocument, x, grp, y, ymaxPic, iScrollbar, iThumb, htmlText, scale, angle, name, nGroup, ph, shHeight, shY, yminPic, gh, shX, gw, height, iTabs, _i, shp, shape, menu, posX, item, dy, my, mouseMove, n, bh, th, yminThumb, ymaxThumb, hPage, group, yMove, iMin, iMax, x1, x2, y1, y2, y3, y4, y5, y6, yLast, myLast, myThumb, shWidth, s, silverlight, fs, alpha, j, gx, gy, _a, scaleX, cx, _x, _y, r, a, ox, oy, color, msWait, xmin, xmax, ymin, ymax As Primitive
    Sub Main()
        ' Small Wiki Pad
        ' Version 0.41a
        ' Copyright © 2015-2017 Nonki Takahashi.  The MIT License.
        ' Last update 2017-08-02
        ' Program ID NVD371-5

        GraphicsWindow.Title = "Small Wiki Pad 0.41a"
        CR = Text.GetCharacter(13)
        LF = Text.GetCharacter(10)
        LT = "<"
        WQ = Text.GetCharacter(34)
        SB_Workaround()
        Form()
        LoadWikiText()
        LoadHTMLText()
        Controls.SetTextBoxText(tbox, wikiText)
        AddHandler GraphicsWindow.MouseDown, AddressOf OnMouseDown
        AddHandler GraphicsWindow.MouseMove, AddressOf OnMouseMove
        While True
            If mouseDown Then
                Controls_TabClick()
                If tabClicked Then
                    ChangeTab()
                Else
                    Controls_ScrollBar()
                End If
                mouseDown = False
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
                    Stack.PushValue("local", i)
                    i = iDocument
                    Group_Hide()
                    x = grp("x")
                    y = ymaxPic
                    Group_Move()
                    i = iScrollbar
                    Group_Hide()
                    i = iThumb
                    Group_Hide()
                    x = grp("x")
                    y = 32 + 14
                    Group_Move()
                    i = Stack.PopValue("local")
                End If
                Controls.ShowControl(tbox)
                If i = 1 Then
                    Controls.SetTextBoxText(tbox, wikiText)
                ElseIf i = 2 Then
                    Controls.SetTextBoxText(tbox, htmlText)
                End If
            ElseIf i = 3 Then
                Stack.PushValue("local", i)
                Controls.HideControl(tbox)
                ' initialize shapes
                If iDocument = CType("", Primitive) Then
                    Shapes_Init()
                    scale = 1
                    angle = 0
                    ' add shapes
                    name = "document"
                    Group_Add()
                    iDocument = nGroup
                    ph = shHeight ' picture height
                    ymaxPic = 32 + shY
                    yminPic = gh - ph
                    x = shX
                    y = ymaxPic
                    i = iDocument
                    Group_Move()
                Else
                    i = iDocument
                    Group_Show()
                End If
                If iScrollBar = CType("", Primitive) Then
                    x = gw - 12
                    y = 32
                    height = gh - y
                    scale = 1
                    Controls_AddVScroll()
                Else
                    i = iScrollBar
                    Group_Show()
                    i = iThumb
                    Group_Show()
                End If
                i = iTabs
                Group_Remove()
                Group_Add()
                For _i = 1 To 3
                    shp = shape(_i * 3)
                    menu(_i) = shp("obj")
                    shp = shape((_i * 3) + 2)
                    hline(_i) = shp("obj")
                Next
                i = Stack.PopValue("local")
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
    Sub LoadWikiText()
        wikiText = "**bold**" + LT + "br>" + CR + LF
        wikiText = wikiText + "_italics_" + LT + "br>" + CR + LF
        wikiText = wikiText + "# Heading 1" + CR + LF
        wikiText = wikiText + "## Heading 2" + CR + LF
        wikiText = wikiText + "### Heading 3" + CR + LF
        wikiText = wikiText + "- Bullet List" + CR + LF
        wikiText = wikiText + "   - Bullet List 2" + CR + LF
        wikiText = wikiText + "1. Number List" + CR + LF
        wikiText = wikiText + "   1. Number List 2" + CR + LF
        wikiText = wikiText + "![](Turtle.png)" + CR + LF
        wikiText = wikiText + "|Table Heading 1|Table Heading 2|" + CR + LF
        wikiText = wikiText + "|---|---|" + CR + LF
        wikiText = wikiText + "|Row 1 - Cell 1|Row 1 - Cell 2|" + CR + LF
        wikiText = wikiText + "|Row 2 - Cell 1|Row 2 - Cell 2|" + CR + LF
        wikiText = wikiText + "___"
    End Sub
    Sub LoadHTMLText()
        htmlText = LT + "html>" + CR + LF
        htmlText = htmlText + LT + "body>" + CR + LF
        htmlText = htmlText + LT + "p>" + CR + LF
        htmlText = htmlText + LT + "strong>bold" + LT + "/strong>" + LT + "br>" + CR + LF
        htmlText = htmlText + LT + "em>italics" + LT + "/em>" + LT + "br>" + CR + LF
        htmlText = htmlText + LT + "/p>" + CR + LF
        htmlText = htmlText + LT + "h1>Heading 1" + LT + "/h1>" + CR + LF
        htmlText = htmlText + LT + "h2>Heading 2" + LT + "/h2>" + CR + LF
        htmlText = htmlText + LT + "h3>Heading 3" + LT + "/h3>" + CR + LF
        htmlText = htmlText + LT + "ul>" + CR + LF
        htmlText = htmlText + LT + "li>Bullet List" + LT + "/li>" + LT + "br>" + CR + LF
        htmlText = htmlText + LT + "ul>" + CR + LF
        htmlText = htmlText + LT + "li>Bullet List 2" + LT + "/li>" + CR + LF
        htmlText = htmlText + LT + "/ul>" + CR + LF
        htmlText = htmlText + LT + "/ul>" + CR + LF
        htmlText = htmlText + LT + "ol>" + CR + LF
        htmlText = htmlText + LT + "li>Number List" + LT + "/li>" + LT + "br>" + CR + LF
        htmlText = htmlText + LT + "ol typr=i>" + CR + LF
        htmlText = htmlText + LT + "li>Number List 2" + LT + "/li>" + CR + LF
        htmlText = htmlText + LT + "/ol>" + CR + LF
        htmlText = htmlText + LT + "/ol>" + CR + LF
        htmlText = htmlText + LT + "p>" + LT + "img src='http://www.nonkit.com/smallbasic.files/Turtle.png'>" + LT + "/p>" + CR + LF
        htmlText = htmlText + LT + "table border=1>" + CR + LF
        htmlText = htmlText + LT + "tr>" + LT + "th>Table Heading 1" + LT + "/th>" + LT + "th>Table Heading 2" + LT + "/th>" + LT + "/tr>" + CR + LF
        htmlText = htmlText + LT + "tr>" + LT + "td>Row 1-Cell 1" + LT + "/td>" + LT + "td>Row 1-Cell 2" + LT + "/td>" + LT + "/tr>" + CR + LF
        htmlText = htmlText + LT + "tr>" + LT + "td>Row 2-Cell 1" + LT + "/td>" + LT + "td>Row 2-Cell 2" + LT + "/td>" + LT + "/tr>" + CR + LF
        htmlText = htmlText + LT + "/table>" + CR + LF
        htmlText = htmlText + LT + "hr>" + CR + LF
        htmlText = htmlText + LT + "/body>" + CR + LF
        htmlText = htmlText + LT + "/html>"
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
    Sub Controls_AddTabs()
        ' param item[] - tab items
        shX = 0 ' x offset
        shY = 0 ' y offset
        shape = ""
        shape(1) = "func=rect;x=0;y=0;width=" + gw + ";height=32;pw=0;bc=#FFFFFF;"
        shape(2) = "func=line;x=" + posX(1) + ";y=5;x1=0;y1=0;x2=" + (posX(4) - posX(1)) + ";y2=0;pw=1;pc=#666666;"
        n = Microsoft.SmallBasic.Library.Array.GetItemCount(item)
        For i = 1 To n
            shape(i * 3) = "func=text;x=" + (posX(i) + 5) + ";y=10;text=" + item(i) + ";fn=Segoe UI;fs=12;"
            shape((i * 3) + 1) = "func=line;x=" + posX(i) + ";y=5;x1=0;y1=0;x2=0;y2=25;pw=1;pc=#666666;"
            shape((i * 3) + 2) = "func=line;x=" + posX(i) + ";y=30;x1=0;y1=0;x2=" + (posX(i + 1) - posX(i)) + ";y2=0;pw=1;pc=#666666;"
        Next
        iLast = 1
        shape((n * 3) + 3) = "func=line;x=" + posX(4) + ";y=5;x1=0;y1=0;x2=0;y2=25;pw=1;pc=#666666;"
        shape((n * 3) + 4) = "func=line;x=" + posX(4) + ";y=30;x1=0;y1=0;x2=" + (gw - 5 - posX(4)) + ";y2=0;pw=1;pc=#666666;"
        name = "tabs"
        Group_Add()
        For i = 1 To n
            shp = shape(i * 3)
            menu(i) = shp("obj")
            shp = shape((i * 3) + 2)
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
        shape = ""
        shape(1) = "func=rect;x=0;y=0;width=14;height=" + height + ";pw=0;bc=#99CCCCCC;"
        shape(2) = "func=rect;x=0;y=0;width=14;height=14;pw=0;bc=#CCCCCC;"
        shape(3) = "func=line;x=3;y=4;x1=0;y1=3;x2=3;y2=0;pw=1;pc=#666666;"
        shape(4) = "func=line;x=6;y=4;x1=0;y1=0;x2=3;y2=3;pw=1;pc=#666666;"
        shape(5) = "func=rect;x=0;y=" + (height - 14) + ";width=14;height=14;pw=0;bc=#CCCCCC;"
        shape(6) = "func=line;x=3;y=" + (height - 8) + ";x1=0;y1=0;x2=3;y2=3;pw=1;pc=#666666;"
        shape(7) = "func=line;x=6;y=" + (height - 8) + ";x1=0;y1=3;x2=3;y2=0;pw=1;pc=#666666;"
        name = "scroll"
        Group_Add()
        iScrollBar = nGroup
        ' add thumb
        bh = height - 28 ' scroll bar height
        th = bh * height / ph ' thumb heignt
        shX = x
        shY = y + 14
        shape = ""
        shape(1) = "func=rect;x=0;y=0;width=14;height=" + th + ";pw=0;bc=#999999;"
        name = "thumb"
        Group_Add()
        iThumb = nGroup
        yminThumb = y + 14
        ymaxThumb = y + height - 14 - th
        hPage = height ' page height in scroll bar
    End Sub
    Sub Controls_Scroll()
        ' param yMove - relative thumb move
        i = iDocument
        grp = group(i)
        x = grp("x")
        y = grp("y") - (yMove * hPage / (ymaxThumb - yminThumb))
        Group_Move()
    End Sub
    Sub Controls_ScrollBar()
        grp = group(iScrollBar)
        shX = grp("x")
        shY = grp("y")
        shape = grp("shape")
        iMin = 1
        iMax = Microsoft.SmallBasic.Library.Array.GetItemCount(shape)
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
            shX = grp("x")
            shY = grp("y")
            shape = grp("shape")
            shp = shape(iMin)
            y5 = shY + shp("ry")
            y6 = shY + shp("ry") + shp("height")
            y = y5
            yLast = y5
            If (y1 <= dy) And (dy <= y2) Then
                ' line up
                x = shX
                y = y5 - hPage
                If y < yminThumb Then
                    y = yminThumb
                End If
            ElseIf (y3 <= dy) And (dy <= y4) Then
                ' line down
                x = shX
                y = y5 + hPage
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
                            i = iThumb
                            Group_Move() ' move thumb
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
                x = shX
                yMove = y - yLast
                i = iThumb
                Group_Move() ' move thumb
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
    Sub Group_Add()
        ' Group | add shapes to a group
        ' param name - group name
        ' param shX, shY, origin of shape array
        ' param shape[] - shape array
        ' param nGroup - number of group
        ' return nGroup - updated number of group
        ' return group - group array
        Stack.PushValue("local", i)
        Stack.PushValue("local", x)
        Stack.PushValue("local", y)
        nGroup = nGroup + 1
        grp = ""
        grp("name") = name
        grp("x") = shX
        grp("y") = shY
        grp("angle") = 0
        grp("dir") = 1
        Shapes_CalcWidthAndHeight()
        grp("width") = shWidth
        grp("cx") = shWidth / 2
        grp("height") = shHeight
        s = 1
        grp("scale") = s
        For i = 1 To Microsoft.SmallBasic.Library.Array.GetItemCount(shape)
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
                If silverlight Then
                    fs = Microsoft.SmallBasic.Library.Math.Floor(shp("fs") * 0.9)
                Else
                    fs = shp("fs")
                End If
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
        grp("shape") = shape
        group(nGroup) = grp
        y = Stack.PopValue("local")
        x = Stack.PopValue("local")
        i = Stack.PopValue("local")
    End Sub
    Sub Group_Dump()
        ' Gourp | Dump a group for debug
        ' param group[i] - group to dump
        grp = group(i)
        TextWindow.WriteLine("name=" + grp("name"))
        TextWindow.WriteLine("x=" + grp("x"))
        TextWindow.WriteLine("y=" + grp("y"))
        TextWindow.WriteLine("cx=" + grp("cx"))
        TextWindow.WriteLine("width=" + grp("width"))
        TextWindow.WriteLine("dir=" + grp("dir"))
        shape = grp("shape")
        For j = 1 To Microsoft.SmallBasic.Library.Array.GetItemCount(shape)
            TextWindow.WriteLine("shape[" + j + "]=" + WQ + shape(j) + WQ)
        Next
    End Sub
    Sub Group_Flip()
        ' Group | Flip a group
        ' param group[i] - group to flip
        ' return group[i] - flipped group
        grp = group(i)
        gx = grp("x")
        gy = grp("y")
        shape = grp("shape")
        n = Microsoft.SmallBasic.Library.Array.GetItemCount(shape)
        For angle = 20 To 180 Step 20
            _a = Microsoft.SmallBasic.Library.Math.GetRadians(angle)
            scaleX = Microsoft.SmallBasic.Library.Math.Cos(_a)
            For j = 1 To n
                shp = shape(j)
                Shapes.Zoom(shp("obj"), Microsoft.SmallBasic.Library.Math.Abs(scaleX), 1)
                cx = shp("x") + (shp("width") / 2)
                cx = ((cx - grp("cx")) * scaleX) + grp("cx")
                Shapes.Move(shp("obj"), cx - (shp("width") / 2) + gx, shp("y") + gy)
                If (angle = 100) And (shp("angle") <> 0) Then
                    shp("angle") = -shp("angle")
                    Shapes.Rotate(shp("obj"), shp("angle"))
                    shape(j) = shp
                End If
                If angle = 180 Then
                    _x = Microsoft.SmallBasic.Library.Math.Floor((cx - (shp("width") / 2)) * 100) / 100
                    shp("rx") = _x
                    shp("x") = _x
                    shape(j) = shp
                End If
            Next
            Program.Delay(20)
        Next
        grp("dir") = grp("dir") * -1
        grp("shape") = shape
        group(i) = grp
    End Sub
    Sub Group_Hide()
        ' Group | Hide a group
        ' param group[i] - the group to hide
        ' return group[i] - the updated group
        grp = group(i)
        shape = grp("shape")
        Stack.PushValue("local", i)
        For i = 1 To Microsoft.SmallBasic.Library.Array.GetItemCount(shape)
            shp = shape(i)
            Shapes.HideShape(shp("obj"))
        Next
        i = Stack.PopValue("local")
    End Sub
    Sub Group_Move()
        ' Group | Move a group
        ' param group[i] - the group to move
        ' param x, y - the position to move
        ' return group[i] - the updated group
        grp = group(i)
        s = grp("scale")
        grp("x") = x
        grp("y") = y
        shape = grp("shape")
        n = Microsoft.SmallBasic.Library.Array.GetItemCount(shape)
        For j = 1 To n
            shp = shape(j)
            If silverlight And Text.IsSubText("tri|line", shp("func")) Then
                _x = shp("wx")
                _y = shp("wy")
            Else
                _x = shp("rx")
                _y = shp("ry")
            End If
            Shapes.Move(shp("obj"), grp("x") + (_x * s), grp("y") + (_y * s))
        Next
        group(i) = grp
    End Sub
    Sub Group_Remove()
        ' Group | Remove a group
        ' param group[i] - the group to hide
        ' return shape [] - the removed shapes
        ' return shX, shY - origin of shape array
        ' return name - removed group name
        grp = group(i)
        shape = grp("shape")
        Stack.PushValue("local", i)
        For i = 1 To Microsoft.SmallBasic.Library.Array.GetItemCount(shape)
            shp = shape(i)
            Shapes.Remove(shp("obj"))
        Next
        shX = grp("x")
        shY = grp("y")
        i = Stack.PopValue("local")
    End Sub
    Sub Group_Show()
        ' Group | Show a group
        ' param group[i] - the group to show
        ' return group[i] - the updated group
        grp = group(i)
        shape = grp("shape")
        Stack.PushValue("local", i)
        For i = 1 To Microsoft.SmallBasic.Library.Array.GetItemCount(shape)
            shp = shape(i)
            Shapes.ShowShape(shp("obj"))
        Next
        i = Stack.PopValue("local")
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
        ElseIf (x = 0) And (y = 0) Then
            a = 0
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
    Sub Shapes_CalcWidthAndHeight()
        ' Shapes | Calculate total width and height of shapes
        ' param shape[] - shape array
        ' return shWidth, shHeight - total size of shapes
        For i = 1 To Microsoft.SmallBasic.Library.Array.GetItemCount(shape)
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
    Sub Shapes_Init()
        ' Shapes | Initialize shapes data
        ' return shX, shY - current position of shapes
        ' return shape - array of shapes
        shX = 10 ' x offset
        shY = 10 ' y offset
        shape = ""
        shape(1) = "func=text;x=0;y=0;fn=Segoe UI;fs=12;fb=True;text=bold;bc=#000000;"
        shape(2) = "func=text;x=0;y=16;fn=Segoe UI;fs=12;fi=True;text=italics;bc=#000000;"
        shape(3) = "func=text;x=0;y=32;fn=Segoe UI;fs=36;fb=True;text=Heading 1;bc=#000000;"
        shape(4) = "func=text;x=0;y=72;fn=Segoe UI;fs=26;fb=True;text=Heading 2;bc=#000000;"
        shape(5) = "func=text;x=0;y=102;fn=Segoe UI;fs=16;fb=True;text=Heading 3;bc=#000000;"
        shape(6) = "func=ell;x=20;y=128;x2=50;y2=54;width=5;height=5;bc=#000000;pw=0;"
        shape(7) = "func=text;x=30;y=122;fn=Segoe UI;fs=12;text=Bullet List;bc=#000000;"
        shape(8) = "func=ell;x=40;y=148;x2=50;y2=54;width=5;height=5;bc=#FFFFFF;pc=#000000;pw=1;"
        shape(9) = "func=text;x=50;y=142;fn=Segoe UI;fs=12;text=Bullet List 2;bc=#000000;"
        shape(10) = "func=text;x=18;y=158;fn=Segoe UI;fs=12;text=1. Number List;bc=#000000;"
        shape(11) = "func=text;x=38;y=174;fn=Segoe UI;fs=12;text=i. Number List 2;bc=#000000;"
        shape(12) = "func=img;x=0;y=190;src=http://www.nonkit.com/smallbasic.files/Turtle.png;"
        shape(13) = "func=rect;x=0;y=442;width=200;height=54;bc=#FFFFFF;pc=#666666;pw=1;"
        shape(14) = "func=rect;x=0;y=460;width=200;height=1;pc=#666666;pw=1;"
        shape(15) = "func=rect;x=0;y=478;width=200;height=1;pc=#666666;pw=1;"
        shape(16) = "func=rect;x=100;y=442;width=1;height=54;pc=#666666;pw=1;"
        shape(17) = "func=text;x=2;y=442;fn=Segoe UI;fs=12;fb=True;text=Table Heading 1;bc=#000000;"
        shape(18) = "func=text;x=102;y=442;fn=Segoe UI;fs=12;fb=True;text=Table Heading 2;bc=#000000;"
        shape(19) = "func=text;x=2;y=460;fn=Segoe UI;fs=12;text=Row 1-Cell 1;bc=#000000;"
        shape(20) = "func=text;x=102;y=460;fn=Segoe UI;fs=12;text=Row 1-Cell 2;bc=#000000;"
        shape(21) = "func=text;x=2;y=478;fn=Segoe UI;fs=12;text=Row 2-Cell 1;bc=#000000;"
        shape(22) = "func=text;x=102;y=478;fn=Segoe UI;fs=12;text=Row 2-Cell 2;bc=#000000;"
        shape(23) = "func=rect;x=0;y=500;width=578;height=1;pc=#666666;pw=1;"
    End Sub
End Module
