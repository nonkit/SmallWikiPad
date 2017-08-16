Module SmallWikiPadModule
    Dim tbox, wikiText, dx, mx, mouseDown, posX, tabClicked, i, lastTabClicked, iLast, hline, shadow, iDocument, x, grp, y, ymaxPic, iScrollbar, iThumb, htmlText, scale, angle, name, nGroup, ph, shHeight, shY, yminPic, gh, shX, gw, height, iTabs, _i, shp, shape, menu, item, CR, LF, LT, TAB, WQ, __Not, fsNormal, fn, fsH, roman, fb, fi, bc, pc, pw, param, x3, txt, paragraph, lineHeight, dy, my, mouseMove, lastWikiText, iMax, n, bh, th, yminThumb, ymaxThumb, hPage, group, yMove, iMin, x1, x2, y1, y2, y3, y4, y5, y6, yLast, myLast, myThumb, yc, fs, y0, x0, px0, px, str, width, _y, _x, color, left, right, s, shWidth, silverlight, alpha, j, r, a, pSave, p, c2, buf, match, delim, _fb, c, lastIndent, indent, _htmlText, c4, _indent, _bc, _pw, nH, _fs, c3, altText, imageName, _fi, caption, nN, type, num, len, cn, _txt, lastParagraph, ox, oy, msWait, func, xmin, xmax, ymin, ymax, fileNameOrUrl, img, _shape, _shX, _shY As Primitive
    Sub Main()
        ' Small Wiki Pad
        ' Version 0.5.1b
        ' Copyright © 2015-2017 Nonki Takahashi.  The MIT License.
        ' Last update 2017-08-15
        ' Program ID NVD371-7

        GraphicsWindow.Title = "Small Wiki Pad 0.5.1b"
        Init()
        Form()
        LoadWikiText()
        Controls.SetTextBoxText(tbox, wikiText)
        AddHandler GraphicsWindow.MouseDown, AddressOf OnMouseDown
        AddHandler GraphicsWindow.MouseMove, AddressOf OnMouseMove
        While true
            If mouseDown Then
                AddHandler GraphicsWindow.MouseDown, AddressOf DoNothing
                Controls_TabClick()
                If tabClicked Then
                    ChangeTab()
                Else
                    Controls_ScrollBar()
                End If
                AddHandler GraphicsWindow.MouseDown, AddressOf OnMouseDown
                mouseDown = false
            End If
        End While







































































    End Sub
    Sub ChangeTab()
        i = lastTabClicked
        If i <> iLast Then
            Shapes.ShowShape(hline(iLast))
            Shapes.SetOpacity(shadow(iLast), 20)
            Shapes.HideShape(hline(i))
            Shapes.SetOpacity(shadow(i), 0)
            If iLast = 1 Then
                Update()
            End If
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
                    Shapes_Restore()
                    scale = 1
                    angle = 0
                    ' add shapes
                    name = "document"
                    Group_Add()
                    iDocument = nGroup
                    ph = shHeight + shY + 6 ' picture height
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
                i = Stack.PopValue("local")
                For _i = 1 To 3
                    shp = shape((_i * 4) - 1)
                    menu(_i) = shp("obj")
                    shp = shape((_i * 4) + 1)
                    hline(_i) = shp("obj")
                    shp = shape((_i * 4) + 2)
                    shadow(_i) = shp("obj")
                    If _i = i Then
                        Shapes.SetOpacity(shadow(_i), 0)
                    Else
                        Shapes.SetOpacity(shadow(_i), 20)
                    End If
                Next
                Shapes.HideShape(hline(i))
            End If
            iLast = i
        End If
    End Sub
    Sub DoNothing()
    End Sub
    Sub Form()
        gw = 598
        gh = 428
        GraphicsWindow.Width = gw
        GraphicsWindow.Height = gh
        GraphicsWindow.BrushColor = "#333333"
        GraphicsWindow.FontName = "Verdana"
        GraphicsWindow.FontBold = false
        GraphicsWindow.PenWidth = 1
        GraphicsWindow.PenColor = "#666666"
        posX = "1=0;2=100;3=200;4=300;"
        item = "1=Wiki;2=HTML;3=Preview;"
        scale = 1
        Controls_AddTabs()
        iTabs = nGroup
        GraphicsWindow.BrushColor = "#000"
        tbox = Controls.AddMultiLineTextBox(posX(1), 31)
        Controls.SetSize(tbox, gw, gh - 31)
    End Sub
    Sub Init()
        CR = Text.GetCharacter(13)
        LF = Text.GetCharacter(10)
        LT = "<"
        TAB = Text.GetCharacter(19)
        WQ = Text.GetCharacter(34)
        __Not = "False=True;True=False;"
        SB_Workaround()
        fsNormal = 16
        fn = "Verdana"
        fsH = "1=32;2=24;3=20;4=16;"
        roman = "1=i;2=ii;3=iii;4=iv;5=v;6=vi;7=vii;8=viii;9=ix;10=x;11=xi;12=xii;"
        fb = false
        fi = false
        bc = "#000000" ' font color (brush color)
        pc = "#000000"
        pw = 0
        param = ""
        shX = 10
        shY = 10
        x3 = 0
        txt = ""
        pw = 0
        paragraph = true
        lineHeight = fsNormal * 1.5
    End Sub
    Sub LoadWikiText()
        wikiText = "**bold**" + LT + "br>" + CR + LF
        wikiText = wikiText + "_italics_" + LT + "br>" + CR + LF
        wikiText = wikiText + "# Heading 1" + CR + LF
        wikiText = wikiText + "## Heading 2" + CR + LF
        wikiText = wikiText + "### Heading 3" + CR + LF
        wikiText = wikiText + "#### Heading 4" + CR + LF
        wikiText = wikiText + "- Bullet List" + CR + LF
        wikiText = wikiText + "    - Bullet List 2" + CR + LF
        wikiText = wikiText + CR + LF
        wikiText = wikiText + "1. Number List" + CR + LF
        wikiText = wikiText + "    1. Number List 2" + CR + LF
        wikiText = wikiText + CR + LF
        wikiText = wikiText + "![Turtle](http://www.nonkit.com/smallbasic.files/Turtle.png)" + CR + LF
        wikiText = wikiText + CR + LF
        wikiText = wikiText + "[SmallWikiPad](http://github.com/nonkit/SmallWikiPad)" + CR + LF
        wikiText = wikiText + CR + LF
        wikiText = wikiText + "| Table Heading 1 | Table Heading 2 |" + CR + LF
        wikiText = wikiText + "| --- | --- |" + CR + LF
        wikiText = wikiText + "| Row 1 - Cell 1 | Row 1 - Cell 2 |" + CR + LF
        wikiText = wikiText + "| Row 2 - Cell 1 | Row 2 - Cell 2 |" + CR + LF
        wikiText = wikiText + "___" + CR + LF
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
    Sub Update()
        wikiText = Controls.GetTextBoxText(tbox)
        If wikiText <> lastWikiText Then
            name = "document"
            Group_Remove()
            shape = ""
            iMax = 0
            x = 0
            y = 0
            lineHeight = fsNormal * 1.5
            Parse_WikiText()
            shX = 10
            shY = 10
            Shapes_Save()
            lastWikiText = wikiText
            iDocument = ""
        End If
    End Sub
    Sub Controls_AddTabs()
        ' param item[] - tab items
        shX = 0 ' x offset
        shY = 0 ' y offset
        shape = ""
        shape(1) = "func=rect;x=0;y=0;width=" + gw + ";height=32;pw=0;bc=#FFF;"
        shape(2) = "func=line;x=" + posX(1) + ";y=1;x1=0;y1=0;x2=" + (posX(4) - posX(1)) + ";y2=0;pw=1;pc=#666;"
        n = Microsoft.SmallBasic.Library.Array.GetItemCount(item)
        For i = 1 To n
            shape((i * 4) - 1) = "func=text;x=" + (posX(i) + 5) + ";y=10;text=" + item(i) + ";fn=Segoe UI;fs=12;"
            shape(i * 4) = "func=line;x=" + posX(i) + ";y=0;x1=0;y1=0;x2=0;y2=31;pw=1;pc=#666;"
            shape((i * 4) + 1) = "func=line;x=" + posX(i) + ";y=31;x1=0;y1=0;x2=" + (posX(i + 1) - posX(i)) + ";y2=0;pw=1;pc=#666;"
            shape((i * 4) + 2) = "func=rect;x=" + posX(i) + ";y=0;width=" + (posX(i + 1) - posX(i)) + ";height=32;pw=0;bc=#000;"
        Next
        iLast = 1
        shape((n * 4) + 3) = "func=line;x=" + posX(4) + ";y=0;x1=0;y1=0;x2=0;y2=31;pw=1;pc=#666;"
        shape((n * 4) + 4) = "func=rect;x=" + (posX(4) + 1) + ";y=0;width=" + (gw - posX(4) - 1) + ";height=32;pw=0;bc=#BBB;"
        shape((n * 4) + 5) = "func=line;x=" + posX(4) + ";y=31;x1=0;y1=0;x2=" + (gw - posX(4)) + ";y2=0;pw=1;pc=#666;"
        name = "tabs"
        Group_Add()
        For i = 1 To n
            shp = shape((i * 4) - 1)
            menu(i) = shp("obj")
            shp = shape((i * 4) + 1)
            hline(i) = shp("obj")
            shp = shape((i * 4) + 2)
            shadow(i) = shp("obj")
            If i = iLast Then
                Shapes.SetOpacity(shadow(i), 0)
            Else
                Shapes.SetOpacity(shadow(i), 20)
            End If
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
        shape(3) = "func=line;x=3;y=6;x1=0;y1=3;x2=3;y2=0;pw=1;pc=#666666;"
        shape(4) = "func=line;x=6;y=6;x1=0;y1=0;x2=3;y2=3;pw=1;pc=#666666;"
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
        Stack.PushValue("local", y)
        i = iDocument
        grp = group(i)
        x = grp("x")
        y = grp("y") - (yMove / bh * ph)
        If y < yminPic Then
            y = yminPic
        ElseIf ymaxPic < y Then
            y = ymaxPic
        End If
        Group_Move()
        y = Stack.PopValue("local")
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
                y = y5 - (th / 25)
                If y < yminThumb Then
                    y = yminThumb
                End If
            ElseIf (y3 <= dy) And (dy <= y4) Then
                ' line down
                x = shX
                y = y5 + (th / 25)
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
                y = y5 - th
                If y < yminThumb Then
                    y = yminThumb
                End If
            ElseIf y6 < dy Then
                ' page down
                x = shX
                y = y5 + th
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
    Sub Font_GetTextWidth()
        ' param fs - font size
        ' param fn - font name
        ' param fb - font bold
        ' param fi - font italic
        ' param txt - text to get width in px
        ' return width - text width
        yc = 4
        GraphicsWindow.BrushColor = "#FFFFFF"
        GraphicsWindow.FillRectangle(0, yc, gw, gh - yc)
        GraphicsWindow.FontSize = fs
        GraphicsWindow.FontName = fn
        GraphicsWindow.FontBold = fb
        GraphicsWindow.FontItalic = fi
        GraphicsWindow.BrushColor = "#FEFEFE"
        GraphicsWindow.DrawText(0, yc, "||")
        y0 = yc
        y1 = yc + fs
        x0 = 0
        x1 = fs * 2
        If gw < x1 Then
            x1 = gw - 1
        End If
        Font_Measure()
        px0 = px
        GraphicsWindow.BrushColor = "#FFFFFF"
        GraphicsWindow.FillRectangle(0, yc, gw, gh - yc)
        GraphicsWindow.BrushColor = "Gray"
        GraphicsWindow.FillRectangle(0, yc + fs, gw, 1)
        str = "|" + txt + "|"
        GraphicsWindow.BrushColor = "#FEFEFE"
        GraphicsWindow.DrawText(0, yc, str)
        x1 = fs * Text.GetLength(str)
        If gw < x1 Then
            x1 = gw - 1
        End If
        Font_Measure()
        width = px - px0
    End Sub
    Sub Font_Measure()
        ' return px - width in pixel (px)
        _y = Microsoft.SmallBasic.Library.Math.Floor((y0 + y1) / 2)
        For _x = x0 To x1
            color = GraphicsWindow.GetPixel(_x, _y)
            If __Not(Text.EndsWith(color, "FFFFFF")) Then
                left = _x
                _x = x1 ' exit For
            End If
        Next
        For _x = x1 To x0 Step -1
            color = GraphicsWindow.GetPixel(_x, _y)
            If __Not(Text.EndsWith(color, "FFFFFF")) Then
                right = _x
                _x = x0 ' exit For
            End If
        Next
        For _x = right To x0 Step -1
            color = GraphicsWindow.GetPixel(_x, _y)
            If Text.EndsWith(color, "FFFFFF") Then
                right = _x + 1
                _x = x0 ' exit For
            End If
        Next
        px = right - left
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
        Stack.PushValue("local", s)
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
                shp("obj") = Shapes.AddImage(shp("img"))
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
        s = Stack.PopValue("local")
        y = Stack.PopValue("local")
        x = Stack.PopValue("local")
        i = Stack.PopValue("local")
    End Sub
    Sub Group_Dump()
        ' Gourp | Dump a group for debug
        ' param group[i] - group to dump
        Stack.PushValue("local", j)
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
        j = Stack.PopValue("local")
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
        Stack.PushValue("local", s)
        Stack.PushValue("local", n)
        Stack.PushValue("local", j)
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
        j = Stack.PopValue("local")
        n = Stack.PopValue("local")
        s = Stack.PopValue("local")
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
    Sub Parse_BlankLine()
        ' EBNF: blank line = [ space ], CR, LF;
        ' return match - "True" if match
        ' return htmlText - null
        Stack.PushValue("local", pSave)
        pSave = p
        Parse_Space()
        c2 = Text.GetSubText(buf, p, 2)
        If c2 = (CR + LF) Then
            htmlText = ""
            match = true
            p = p + 2
        Else
            match = false
            p = pSave
        End If
        pSave = Stack.PopValue("local")
    End Sub
    Sub Parse_BlankLines()
        ' EBNF: blank lines = blank line, { blank line };
        ' return match - "True" if match
        ' return htmlText - null
        Parse_BlankLine()
        If match Then
            While match
                Parse_BlankLine()
            End While
            match = true
        End If
        If match Then
            If paragraph Then
                y = y + lineHeight + fsNormal
                x = 0
                lineHeight = fsNormal * 1.5
            End If
            y = y + fsNormal
        End If
    End Sub
    Sub Parse_Block()
        ' EBNF: block = heading | bullet list | number list | table | horizontal rule | blank lines | line;
        ' return match - "True" if match
        ' return htmlText - HTML text of block
        ' return paragraph - "True" if match with line
        Parse_Heading()
        If __Not(match) Then
            Parse_BulletList()
        End If
        If __Not(match) Then
            Parse_NumberList()
        End If
        If __Not(match) Then
            Parse_Table()
        End If
        If __Not(match) Then
            Parse_HorizontalRule()
        End If
        If __Not(match) Then
            Parse_BlankLines()
        End If
        If match Then
            paragraph = false
        Else
            Parse_Line()
            If match Then
                paragraph = true
            End If
        End If
    End Sub
    Sub Parse_Bold()
        ' EBNF: bold = '**', ( italic | text ), '**';
        ' return match - "True" if match
        ' return htmlText - bold
        Stack.PushValue("local", pSave)
        pSave = p
        c2 = Text.GetSubText(buf, p, 2)
        If c2 = CType("**", Primitive) Then
            p = p + 2
            match = true
        Else
            match = false
        End If
        If match Then
            Parse_Italic()
            If __Not(match) Then
                delim = "**"
                Parse_Text()
                If match Then
                    htmlText = txt
                End If
            End If
        End If
        If match Then
            c2 = Text.GetSubText(buf, p, 2)
            If c2 = CType("**", Primitive) Then
                p = p + 2
            Else
                match = false
            End If
        End If
        If match Then
            htmlText = LT + "strong>" + htmlText + LT + "/strong>"
            _fb = fb
            fb = true
            fs = fsNormal
            Shapes_AddText()
            x = x + width
            fb = _fb
        Else
            p = pSave
        End If
        pSave = Stack.PopValue("local")
    End Sub
    Sub Parse_Break()
        ' EBNF: break = '<', ('B' | 'b'), ('R' | 'r'), '>';
        ' return match - "True" if match
        ' return htmlText - HTML text of break
        Stack.PushValue("local", pSave)
        pSave = p
        c = Text.GetSubText(buf, p, 1)
        If c = LT Then
            p = p + 1
            match = true
        Else
            match = false
        End If
        If match Then
            c = Text.GetSubText(buf, p, 1)
            If Text.IsSubText("Bb", c) Then
                p = p + 1
            Else
                match = false
            End If
        End If
        If match Then
            c = Text.GetSubText(buf, p, 1)
            If Text.IsSubText("Rr", c) Then
                p = p + 1
            Else
                match = false
            End If
        End If
        If match Then
            c = Text.GetSubText(buf, p, 1)
            If c = CType(">", Primitive) Then
                p = p + 1
            Else
                match = false
            End If
        End If
        If match Then
            htmlText = LT + "br>"
            y = y + lineHeight
            lineHeight = fs * 1.5
            x = 0
        Else
            p = pSave
        End If
        pSave = Stack.PopValue("local")
    End Sub
    Sub Parse_BulletList()
        ' EBNF: bullet list = bullet list item, { bullet list item }, CR, LF;
        ' return match - "True" if match
        ' return htmlText - HTML text of bullet list
        Stack.PushValue("local", pSave)
        Stack.PushValue("local", htmlText)
        pSave = p
        lastIndent = -1
        Parse_BulletListItem()
        If match Then
            While lastIndent < indent
                htmlText = LT + "ul>" + CR + LF + htmlText
                lastIndent = lastIndent + 1
            End While
            While match
                Stack.PushValue("local", htmlText)
                Parse_BulletListItem()
                _htmlText = Stack.PopValue("local")
                If match Then
                    While lastIndent < indent
                        htmlText = LT + "ul>" + CR + LF + htmlText
                        lastIndent = lastIndent + 1
                    End While
                    While indent < lastIndent
                        htmlText = LT + "/ul>" + CR + LF + htmlText
                        lastIndent = lastIndent - 1
                    End While
                    htmlText = Text.Append(_htmlText, htmlText)
                Else
                    htmlText = _htmlText
                End If
            End While
            match = true
        End If
        If match Then
            c2 = Text.GetSubText(buf, p, 2)
            If c2 = (CR + LF) Then
                p = p + 2
            Else
                match = false
            End If
        End If
        _htmlText = Stack.PopValue("local")
        If match Then
            While -1 < indent
                htmlText = htmlText + LT + "/ul>" + CR + LF
                indent = indent - 1
            End While
        Else
            htmlText = _htmlText
            p = pSave
        End If
        pSave = Stack.PopValue("local")
    End Sub
    Sub Parse_BulletListItem()
        ' EBNF: bullet list item = { '    ' }, '-', space, text, CR, LF;
        ' return match - "True" if match
        ' return indent - number of indent
        ' return htmlText - HTML text of bullet list item
        Stack.PushValue("local", pSave)
        Stack.PushValue("local", indent)
        pSave = p
        indent = 0
        c4 = Text.GetSubText(buf, p, 4)
        While c4 = CType("    ", Primitive)
            indent = indent + 1
            p = p + 4
            c4 = Text.GetSubText(buf, p, 4)
        End While
        c = Text.GetSubText(buf, p, 1)
        If c = CType("-", Primitive) Then
            p = p + 1
            match = true
        Else
            match = false
        End If
        If match Then
            Parse_Space()
        End If
        If match Then
            delim = ""
            Parse_Text()
        End If
        If match Then
            c2 = Text.GetSubText(buf, p, 2)
            If c2 = (CR + LF) Then
                p = p + 2
            Else
                match = false
            End If
        End If
        _indent = Stack.PopValue("local")
        If match Then
            htmlText = LT + "li>" + txt + LT + "/li>" + CR + LF
            x = (indent * 20) + 20
            If paragraph Then
                y = y + lineHeight + fsNormal
                x = 0
                lineHeight = fsNormal * 1.5
            End If
            y = y + 7
            width = 5
            height = 5
            _bc = bc
            _pw = pw
            If indent = 1 Then
                bc = "#FFFFFF"
                pw = 1
            End If
            Shapes_AddEllipse()
            bc = _bc
            pw = _pw
            y = y - 7
            x = x + 10
            Shapes_AddText()
            lineHeight = Microsoft.SmallBasic.Library.Math.Max(lineHeight, fs * 1.5)
            y = y + lineHeight
            lineHeight = fsNormal * 1.5
        Else
            p = pSave
            indent = _indent
        End If
        pSave = Stack.PopValue("local")
    End Sub
    Sub Parse_Character()
        ' EBNF: character = '&' | '<' | escape character | normal character;
        ' return match - "True" if match
        ' return c - character
        c = Text.GetSubText(buf, p, 1)
        If c = CType("&", Primitive) Then
            p = p + 1
            match = true
        Else
            match = false
        End If
        If __Not(match) Then
            c = Text.GetSubText(buf, p, 1)
            If c = LT Then
                p = p + 1
                match = true
            Else
                match = false
            End If
        End If
        If __Not(match) Then
            Parse_EscapeCharacter()
        End If
        If __Not(match) Then
            Parse_NormalCharacter()
        End If
    End Sub
    Sub Parse_Column()
        ' EBNF: column = rich text, '|';
        ' return match - "True" if match
        ' return htmlText - HTML text of column
        Stack.PushValue("local", pSave)
        pSave = p
        delim = "|"
        Parse_RichText()
        If match Then
            c = Text.GetSubText(buf, p, 1)
            If c = CType("|", Primitive) Then
                p = p + 1
            Else
                match = false
            End If
        End If
        If match Then
            htmlText = LT + "td>" + htmlText + LT + "/td>"
        Else
            p = pSave
        End If
        pSave = Stack.PopValue("local")
    End Sub
    Sub Parse_EscapeCharacter()
        ' EBNF: escape character = '\', single character;
        ' return match - "True" if match
        ' return c - single character
        Stack.PushValue("local", pSave)
        pSave = p
        match = false
        c = Text.GetSubText(buf, p, 1)
        If c = CType("\", Primitive) Then
            p = p + 1
            match = true
        Else
            match = false
        End If
        If match Then
            c = Text.GetSubText(buf, p, 1)
            If Text.GetCharacterCode(p) < 256 Then
                p = p + 1
                match = true
            Else
                match = false
            End If
        End If
        If __Not(match) Then
            p = pSave
        End If
        pSave = Stack.PopValue("local")
    End Sub
    Sub Parse_Heading()
        ' EBNF: heading = '#', { '#' }, space, text, CR, LF;
        ' return match - "True" if match
        ' return htmlText - HTML text of heading
        Stack.PushValue("local", pSave)
        pSave = p
        nH = 0
        c = Text.GetSubText(buf, p, 1)
        If c = CType("#", Primitive) Then
            nH = 1
            p = p + 1
            match = true
        Else
            match = false
        End If
        If match Then
            c = Text.GetSubText(buf, p, 1)
            While c = CType("#", Primitive)
                nH = nH + 1
                p = p + 1
                c = Text.GetSubText(buf, p, 1)
            End While
            match = true
        End If
        If match Then
            Parse_Space()
        End If
        If match Then
            delim = ""
            Parse_Text()
        End If
        If match Then
            c2 = Text.GetSubText(buf, p, 2)
            If c2 = (CR + LF) Then
                p = p + 2
            Else
                match = false
            End If
        End If
        If match Then
            htmlText = LT + "h" + nH + ">" + txt + LT + "/h" + nH + ">" + CR + LF
            If paragraph Then
                y = y + lineHeight + fsNormal
                x = 0
                lineHeight = fsNormal * 1.5
            End If
            _fb = fb
            fb = true
            _fs = fs
            fs = fsH(nH)
            Shapes_AddText()
            fb = _fb
            lineHeight = Microsoft.SmallBasic.Library.Math.Max(lineHeight, fs * 1.5)
            fs = _fs
            y = y + lineHeight
            x = 0
            lineHeight = fsNormal * 1.5
        Else
            p = pSave
        End If
        pSave = Stack.PopValue("local")
    End Sub
    Sub Parse_HorizontalRule()
        ' EBNF: horizontal rule = '___', CR, LF;
        ' return match - "True" if match
        ' return htmlText
        Stack.PushValue("local", pSave)
        pSave = p
        c3 = Text.GetSubText(buf, p, 3)
        If c3 = CType("___", Primitive) Then
            p = p + 3
            match = true
        Else
            match = false
        End If
        If match Then
            c2 = Text.GetSubText(buf, p, 2)
            If c2 = (CR + LF) Then
                p = p + 2
            Else
                match = false
            End If
        End If
        If match Then
            htmlText = LT + "hr>" + CR + LF
            If paragraph Then
                y = y + lineHeight + fsNormal
                x = 0
                lineHeight = fsNormal * 1.5
            End If
            height = 2
            width = gw - 20
            pw = 0
            _bc = bc
            bc = "#666666"
            Shapes_AddRectangle()
            bc = _bc
            y = y + 5
            x = 0
        Else
            p = pSave
        End If
        pSave = Stack.PopValue("local")
    End Sub
    Sub Parse_Image()
        ' EBNF: image = '![', [ text ], '](', text, ')';
        ' return match - "True" if match
        ' return htmlText - HTML text
        Stack.PushValue("local", pSave)
        pSave = p
        c2 = Text.GetSubText(buf, p, 2)
        If c2 = CType("![", Primitive) Then
            p = p + 2
            match = true
        Else
            match = false
        End If
        If match Then
            delim = "]("
            Parse_Text()
            If match Then
                altText = txt
            Else
                altText = ""
            End If
            match = true
        End If
        If match Then
            c2 = Text.GetSubText(buf, p, 2)
            If c2 = CType("](", Primitive) Then
                p = p + 2
                match = true
            Else
                match = false
            End If
        End If
        If match Then
            delim = ")"
            Parse_Text()
        End If
        If match Then
            c = Text.GetSubText(buf, p, 1)
            If c = CType(")", Primitive) Then
                p = p + 1
            Else
                match = false
            End If
        End If
        If match Then
            imageName = txt
            htmlText = LT + "img src='" + txt + "' alt='" + altText + "'>"
            If paragraph Then
                y = y + lineHeight + fsNormal
                x = 0
                lineHeight = fsNormal * 1.5
            End If
            Shapes_AddImage()
            lineHeight = Microsoft.SmallBasic.Library.Math.Max(shp("height") + (fsNormal * 0.5), lineHeight)
            x = x + shp("width")
        Else
            p = pSave
        End If
        pSave = Stack.PopValue("local")
    End Sub
    Sub Parse_Italic()
        ' EBNF: italic = '_', ( bold | text ), '_';
        ' return match - "True" if match
        ' return htmlText - HTML text
        Stack.PushValue("local", pSave)
        pSave = p
        c = Text.GetSubText(buf, p, 1)
        If c = CType("_", Primitive) Then
            p = p + 1
            match = true
        Else
            match = false
        End If
        If match Then
            Parse_Bold()
            If __Not(match) Then
                delim = "_"
                Parse_Text()
                If match Then
                    htmlText = txt
                End If
            End If
        End If
        If match Then
            c = Text.GetSubText(buf, p, 1)
            If c = CType("_", Primitive) Then
                p = p + 1
            Else
                match = false
            End If
        End If
        If match Then
            htmlText = LT + "em>" + htmlText + LT + "/em>"
            _fi = fi
            fi = true
            fs = fsNormal
            Shapes_AddText()
            x = x + width
            fi = _fi
        Else
            p = pSave
        End If
        pSave = Stack.PopValue("local")
    End Sub
    Sub Parse_Line()
        ' EBNF: line = rich text, CR, LF;
        ' return match - "True" if match
        ' return htmlText - HTML text
        Stack.PushValue("local", pSave)
        pSave = p
        delim = ""
        Parse_RichText()
        If match Then
            c2 = Text.GetSubText(buf, p, 2)
            If c2 = (CR + LF) Then
                p = p + 2
            Else
                match = false
            End If
        End If
        If match Then
            htmlText = htmlText + CR + LF
        Else
            p = pSave
        End If
        pSave = Stack.PopValue("local")
    End Sub
    Sub Parse_Link()
        ' EBNF: link = '[', [ text ], '](', text, ')';
        ' return match - "True" if match
        ' return htmlText - HTML text
        Stack.PushValue("local", pSave)
        Stack.PushValue("local", htmlText)
        pSave = p
        c = Text.GetSubText(buf, p, 1)
        If c = CType("[", Primitive) Then
            p = p + 1
            match = true
        Else
            match = false
        End If
        If match Then
            delim = "]("
            Parse_Text()
            If match Then
                caption = txt
            Else
                caption = ""
            End If
            match = true
        End If
        If match Then
            c2 = Text.GetSubText(buf, p, 2)
            If c2 = CType("](", Primitive) Then
                p = p + 2
                match = true
            Else
                match = false
            End If
        End If
        If match Then
            delim = ")"
            Parse_Text()
        End If
        If match Then
            c = Text.GetSubText(buf, p, 1)
            If c = CType(")", Primitive) Then
                p = p + 1
            Else
                match = false
            End If
        End If
        _htmlText = Stack.PopValue("local")
        If match Then
            htmlText = LT + "a href='" + txt + "'>" + caption + LT + "/a>"
            txt = caption
            Stack.PushValue("local", bc)
            bc = "Blue"
            Shapes_AddText()
            bc = Stack.PopValue("local")
            x = x + width
        Else
            p = pSave
            htmlText = _htmlText
        End If
        pSave = Stack.PopValue("local")
    End Sub
    Sub Parse_NormalCharacter()
        ' EBNF: normal character = any character;
        ' return match - "True" if match
        ' return c - normal character
        c = Text.GetSubText(buf, p, 1)
        If c <> CType("", Primitive) Then
            match = true
            p = p + 1
        Else
            match = false
        End If
    End Sub
    Sub Parse_Number()
        ' EBNF: number = digit, { digit };
        ' EBNF: digit = "0" | "1" | "2" | "3" | "4" | "5" | "6" | "7" | "8" | "9" ;
        ' return match - "True" if match
        ' return htmlText - HTML text
        c = Text.GetSubText(buf, p, 1)
        match = false
        htmlText = ""
        While Text.IsSubText("0123456789", c)
            htmlText = Text.Append(htmlText, c)
            match = true
            p = p + 1
            c = Text.GetSubText(buf, p, 1)
        End While
    End Sub
    Sub Parse_NumberList()
        ' EBNF: number list = number list item, { number list item }, CR, LF;
        ' return match - "True" if match
        ' return htmlText - HTML text
        Stack.PushValue("local", pSave)
        Stack.PushValue("local", htmlText)
        pSave = p
        lastIndent = -1
        nN = ""
        Parse_NumberListItem()
        If match Then
            While lastIndent < indent
                If lastIndent = 0 Then
                    type = " type='i'"
                Else
                    type = ""
                End If
                htmlText = LT + "ol" + type + ">" + CR + LF + htmlText
                lastIndent = lastIndent + 1
            End While
            While match
                Stack.PushValue("local", htmlText)
                Parse_NumberListItem()
                _htmlText = Stack.PopValue("local")
                If match Then
                    If lastIndent = indent Then
                    End If
                    While lastIndent < indent
                        If lastIndent = 0 Then
                            type = " type='i'"
                        Else
                            type = ""
                        End If
                        htmlText = LT + "ol" + type + ">" + CR + LF + htmlText
                        lastIndent = lastIndent + 1
                    End While
                    While indent < lastIndent
                        htmlText = LT + "/ol>" + CR + LF + htmlText
                        lastIndent = lastIndent - 1
                    End While
                    htmlText = Text.Append(_htmlText, htmlText)
                Else
                    htmlText = _htmlText
                End If
            End While
            match = true
        End If
        If match Then
            c2 = Text.GetSubText(buf, p, 2)
            If c2 = (CR + LF) Then
                p = p + 2
            Else
                match = false
                p = pSave
            End If
        End If
        _htmlText = Stack.PopValue("local")
        If match Then
            While -1 < indent
                htmlText = htmlText + LT + "/ol>" + CR + LF
                indent = indent - 1
            End While
            x = 0
        Else
            htmlText = _htmlText
            p = pSave
        End If
        pSave = Stack.PopValue("local")
    End Sub
    Sub Parse_NumberListItem()
        ' EBNF: number list item = { '    ' }, number, '.', space, text, CR, LF;
        ' return match - "True" if match
        ' return indent - number of indent
        ' return htmlText - HTML text
        Stack.PushValue("local", pSave)
        Stack.PushValue("local", indent)
        pSave = p
        indent = 0
        c4 = Text.GetSubText(buf, p, 4)
        While c4 = CType("    ", Primitive)
            indent = indent + 1
            p = p + 4
            c4 = Text.GetSubText(buf, p, 4)
        End While
        If lastIndent < indent Then
            nN(indent) = 0
        End If
        Parse_Number()
        If match Then
            c = Text.GetSubText(buf, p, 1)
            If c = CType(".", Primitive) Then
                p = p + 1
            Else
                match = false
            End If
        End If
        If match Then
            Parse_Space()
        End If
        If match Then
            delim = ""
            Parse_Text()
        End If
        If match Then
            c2 = Text.GetSubText(buf, p, 2)
            If c2 = (CR + LF) Then
                p = p + 2
            Else
                match = false
            End If
        End If
        _indent = Stack.PopValue("local")
        If match Then
            htmlText = LT + "li>" + txt + LT + "/li>" + CR + LF
            x = (indent * 20) + 18
            nN(indent) = nN(indent) + 1
            If indent = 1 Then
                num = roman(nN(1))
            Else
                num = nN(indent)
            End If
            txt = Text.Append(Text.Append(num, ". "), txt)
            Shapes_AddText()
            lineHeight = Microsoft.SmallBasic.Library.Math.Max(lineHeight, fs * 1.5)
            y = y + lineHeight
            x = 0
            lineHeight = fsNormal * 1.5
        Else
            p = pSave
            indent = _indent
        End If
        pSave = Stack.PopValue("local")
    End Sub
    Sub Parse_RichText()
        ' EBNF: rich text = span, { span - delimiter};
        ' param delim - delimiter
        ' return match - "True" if match
        ' return htmlText - HTML text of rich text
        Parse_Span()
        If match Then
            While match
                If delim <> CType("", Primitive) Then
                    len = Text.GetLength(delim)
                    cn = Text.GetSubText(buf, p, len)
                    If cn = delim Then
                        match = false
                    End If
                End If
                If match Then
                    Stack.PushValue("local", htmlText)
                    Stack.PushValue("local", txt)
                    Parse_Span()
                    _txt = Stack.PopValue("local")
                    _htmlText = Stack.PopValue("local")
                    If match Then
                        htmlText = Text.Append(_htmlText, htmlText)
                        txt = Text.Append(_txt, txt)
                    Else
                        htmlText = _htmlText
                        txt = _txt
                    End If
                End If
            End While
            match = true
        End If
    End Sub
    Sub Parse_Row()
        ' EBNF: row = '|', column, { column }, CR, LF;
        ' return match - "True" if match
        Stack.PushValue("local", pSave)
        pSave = p
        c = Text.GetSubText(buf, p, 1)
        If c = CType("|", Primitive) Then
            p = p + 1
            match = true
        Else
            match = false
        End If
        If match Then
            Parse_Column()
        End If
        If match Then
            While match
                Stack.PushValue("local", htmlText)
                Parse_Column()
                _htmlText = Stack.PopValue("local")
                If match Then
                    htmlText = _htmlText + htmlText
                Else
                    htmlText = _htmlText
                End If
            End While
            match = true
        End If
        If match Then
            c2 = Text.GetSubText(buf, p, 2)
            If c2 = (CR + LF) Then
                p = p + 2
            Else
                match = false
            End If
        End If
        If match Then
            htmlText = LT + "tr>" + htmlText + LT + "/tr>" + CR + LF
        Else
            p = pSave
        End If
        pSave = Stack.PopValue("local")
    End Sub
    Sub Parse_Separator()
        ' EBNF: separator = '|', separator column, { separator column }, CR, LF;
        ' return match - "True" if match
        ' return htmlText
        Stack.PushValue("local", pSave)
        pSave = p
        c = Text.GetSubText(buf, p, 1)
        If c = CType("|", Primitive) Then
            p = p + 1
            match = true
        Else
            match = false
        End If
        If match Then
            Parse_SeparatorColumn()
        End If
        If match Then
            While match
                Parse_SeparatorColumn()
            End While
            match = true
        End If
        If match Then
            c2 = Text.GetSubText(buf, p, 2)
            If c2 = (CR + LF) Then
                p = p + 2
            Else
                match = false
            End If
        End If
        If __Not(match) Then
            p = pSave
        End If
        pSave = Stack.PopValue("local")
    End Sub
    Sub Parse_SeparatorColumn()
        ' EBNF: separator column = space, '---', space, '|';
        ' return match - "True" if match
        ' return htmlText
        Stack.PushValue("local", pSave)
        pSave = p
        Parse_Space()
        If match Then
            c3 = Text.GetSubText(buf, p, 3)
            If c3 = CType("---", Primitive) Then
                p = p + 3
            Else
                match = false
            End If
        End If
        If match Then
            Parse_Space()
        End If
        If match Then
            c = Text.GetSubText(buf, p, 1)
            If c = CType("|", Primitive) Then
                p = p + 1
            Else
                match = false
            End If
        End If
        If match Then
        Else
            p = pSave
        End If
        pSave = Stack.PopValue("local")
    End Sub
    Sub Parse_Space()
        ' EBNF: space = { ' ' | TAB };
        ' return match - "True" if match
        ' return c - space
        match = false
        c = Text.GetSubText(buf, p, 1)
        While (c = CType(" ", Primitive)) Or (c = TAB)
            match = true
            p = p + 1
            c = Text.GetSubText(buf, p, 1)
        End While
        If match Then
            c = " "
        Else
            c = ""
        End If
    End Sub
    Sub Parse_Span()
        ' EBNF: span = bold | italic | image | link | break | token;
        ' return match - "True" if match
        ' return htmlText
        Parse_Bold()
        If __Not(match) Then
            Parse_Italic()
        End If
        If __Not(match) Then
            Parse_Image()
        End If
        If __Not(match) Then
            Parse_Link()
        End If
        If __Not(match) Then
            Parse_Break()
        End If
        If __Not(match) Then
            Parse_Token()
            If match Then
                htmlText = txt
                If __Not(paragraph) Then
                    y = y + lineHeight + fsNormal
                    x = 0
                    lineHeight = fsNormal * 1.5
                    paragraph = true
                End If
                Shapes_AddText()
                x = x + width
            End If
        End If
    End Sub
    Sub Parse_Table()
        ' EBNF: table = row, separator, row, { row };
        ' return match - "True" if match
        ' return htmlText
        Stack.PushValue("local", pSave)
        pSave = p
        Parse_Row()
        If match Then
            Parse_Separator()
        End If
        If match Then
            Stack.PushValue("local", htmlText)
            Parse_Row()
            _htmlText = Stack.PopValue("local")
            If match Then
                htmlText = _htmlText + htmlText
            Else
                htmlText = _htmlText
            End If
        End If
        If match Then
            While match
                Stack.PushValue("local", htmlText)
                Parse_Row()
                _htmlText = Stack.PopValue("local")
                If match Then
                    htmlText = _htmlText + htmlText
                Else
                    htmlText = _htmlText
                End If
            End While
            match = true
        End If
        If match Then
            htmlText = LT + "table border='1'>" + CR + LF + htmlText + LT + "/table>" + CR + LF
            If paragraph Then
                y = y + lineHeight + fsNormal
                x = 0
                lineHeight = fsNormal * 1.5
            End If
            y = y + lineHeight + fsNormal
            x = 0
        Else
            p = pSave
        End If
        pSave = Stack.PopValue("local")
    End Sub
    Sub Parse_Text()
        ' EBNF: text = token, { token - delimiter };
        ' param delim - delimiter
        ' return match - "True" if match
        ' return txt - text
        Parse_Token()
        If match Then
            While match
                If delim <> CType("", Primitive) Then
                    len = Text.GetLength(delim)
                    cn = Text.GetSubText(buf, p, len)
                    If cn = delim Then
                        match = false
                    End If
                End If
                If match Then
                    Stack.PushValue("local", txt)
                    Parse_Token()
                    _txt = Stack.PopValue("local")
                    If match Then
                        txt = Text.Append(_txt, txt)
                    Else
                        txt = _txt
                    End If
                End If
            End While
            match = true
        End If
    End Sub
    Sub Parse_Token()
        ' EBNF: token = character - ( '|' | CR | LF ), { character - ( '&' | '<' | '*' | '_' | '[' | ']' | ')' | '!' | '|' | ' ' | TAB | CR | LF ) };
        ' return match - "True" if match
        ' return txt - token
        txt = ""
        c = Text.GetSubText(buf, p, 1)
        If Text.IsSubText("|" + CR + LF, c) Then
            match = false
        Else
            match = true
        End If
        If match Then
            Parse_Character()
        End If
        If match Then
            If c = CType("&", Primitive) Then
                txt = "&amp;"
            ElseIf c = CType("<", Primitive) Then
                txt = "&lt;"
            Else
                txt = c
            End If
            While match
                c = Text.GetSubText(buf, p, 1)
                If Text.IsSubText("&*_[])!| " + TAB + LT + CR + LF, c) Then
                    match = false
                End If
                If match Then
                    Parse_Character()
                End If
                If match Then
                    txt = Text.Append(txt, c)
                End If
            End While
            match = true
        End If
        If __Not(match) Then
            txt = ""
        End If
    End Sub
    Sub Parse_WikiText()
        ' EBNF: Wiki text = { block };
        ' param wikiText
        ' return match - "True" if match
        ' return htmlText
        fs = fsNormal
        buf = wikiText
        p = 1
        Parse_Block()
        If match And paragraph Then
            htmlText = LT + "p>" + htmlText
        End If
        While match
            Stack.PushValue("local", htmlText)
            lastParagraph = paragraph
            Parse_Block()
            _htmlText = Stack.PopValue("local")
            If match Then
                If __Not(lastParagraph) And paragraph Then
                    _htmlText = _htmlText + LT + "p>"
                End If
                If lastParagraph And __Not(paragraph) Then
                    _htmlText = _htmlText + LT + "/p>" + CR + LF
                    lineHeight = fsNormal * 1.5
                    x = 0
                End If
                htmlText = Text.Append(_htmlText, htmlText)
            Else
                htmlText = _htmlText
            End If
        End While
        match = true
        If paragraph Then
            htmlText = htmlText + LT + "/p>" + CR + LF
        End If
        htmlText = LT + "html>" + CR + LF + LT + "body>" + CR + LF + htmlText + LT + "/body>" + CR + LF + LT + "/html>" + CR + LF
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
    Sub Shapes_AddEllipse()
        ' param x, y - top left position
        ' param width
        ' param height
        Shapes_EntryClear()
        Shapes_PenToEntry()
        Shapes_BrushToEntry()
        func = "ell"
        Shapes_FuncToEntry()
        Shapes_MoveToEntry()
        Shapes_RotateToEntry()
        Shapes_EntryToArray()
    End Sub
    Sub Shapes_AddImage()
        ' param x, y - top left position
        ' param imageName - image url
        Shapes_EntryClear()
        func = "img"
        Shapes_FuncToEntry()
        Shapes_MoveToEntry()
        Shapes_RotateToEntry()
        Shapes_EntryToArray()
    End Sub
    Sub Shapes_AddRectangle()
        ' param x, y - top left position
        ' param width
        ' param height
        Shapes_EntryClear()
        Shapes_PenToEntry()
        Shapes_BrushToEntry()
        func = "rect"
        Shapes_FuncToEntry()
        Shapes_MoveToEntry()
        Shapes_RotateToEntry()
        Shapes_EntryToArray()
    End Sub
    Sub Shapes_AddText()
        ' param x, y - top left position
        ' param txt - text
        Shapes_EntryClear()
        Shapes_PenToEntry()
        Shapes_BrushToEntry()
        Shapes_FontToEntry()
        func = "text"
        Shapes_FuncToEntry()
        Shapes_MoveToEntry()
        Shapes_RotateToEntry()
        Shapes_EntryToArray()
    End Sub
    Sub Shapes_BrushToEntry()
        shp("bc") = bc
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
    Sub Shapes_DumpArray()
        Stack.PushValue("local", buf)
        buf = ""
        If 0 < shX Then
            buf = buf + "  shX = " + shX + LF
        End If
        If 0 < shY Then
            buf = buf + "  shY = " + shY + LF
        End If
        For i = iMin To iMax
            buf = buf + "  shape[" + i + "] = " + WQ + shape(i) + WQ + LF
        Next
        TextWindow.WriteLine(buf)
        buf = Stack.PopValue("local")
    End Sub
    Sub Shapes_EntryClear()
        shp = ""
    End Sub
    Sub Shapes_EntryToArray()
        iMax = iMax + 1
        shape(iMax) = shp
    End Sub
    Sub Shapes_FontToEntry()
        shp("fs") = fs
        shp("fn") = fn
        shp("fb") = fb
        shp("fi") = fi
    End Sub
    Sub Shapes_FuncToEntry()
        shp("func") = func
        If func = CType("ell", Primitive) Then
            shp("width") = Microsoft.SmallBasic.Library.Math.Floor(width * 100) / 100
            shp("height") = Microsoft.SmallBasic.Library.Math.Floor(height * 100) / 100
        ElseIf func = CType("rect", Primitive) Then
            shp("width") = Microsoft.SmallBasic.Library.Math.Floor(width * 100) / 100
            shp("height") = Microsoft.SmallBasic.Library.Math.Floor(height * 100) / 100
        ElseIf func = CType("tri", Primitive) Then
            shp("x1") = Microsoft.SmallBasic.Library.Math.Floor(x1 * 100) / 100
            shp("y1") = Microsoft.SmallBasic.Library.Math.Floor(y1 * 100) / 100
            shp("x2") = Microsoft.SmallBasic.Library.Math.Floor(x2 * 100) / 100
            shp("y2") = Microsoft.SmallBasic.Library.Math.Floor(y2 * 100) / 100
            shp("x3") = Microsoft.SmallBasic.Library.Math.Floor(x3 * 100) / 100
            shp("y3") = Microsoft.SmallBasic.Library.Math.Floor(y3 * 100) / 100
        ElseIf func = CType("line", Primitive) Then
            shp("x1") = Microsoft.SmallBasic.Library.Math.Floor(x1 * 100) / 100
            shp("y1") = Microsoft.SmallBasic.Library.Math.Floor(y1 * 100) / 100
            shp("x2") = Microsoft.SmallBasic.Library.Math.Floor(x2 * 100) / 100
            shp("y2") = Microsoft.SmallBasic.Library.Math.Floor(y2 * 100) / 100
        ElseIf func = CType("text", Primitive) Then
            shp("text") = txt
            Font_GetTextWidth()
            shp("width") = width
            shp("height") = fs
        ElseIf func = CType("img", Primitive) Then
            shp("src") = imageName
            If Text.IsSubText(imageName, ":") Then
                fileNameOrUrl = imageName
            Else
                fileNameOrUrl = Program.Directory + "/" + imageName
            End If
            img = ImageList.LoadImage(fileNameOrUrl)
            shp("img") = img
            shp("width") = ImageList.GetWidthOfImage(img)
            shp("height") = ImageList.GetHeightOfImage(img)
        End If
    End Sub
    Sub Shapes_Init()
        ' Shapes | Initialize shapes data
        ' return shX, shY - current position of shapes
        ' return shape - array of shapes
        shX = 10 ' x offset
        shY = 10 ' y offset
        shape = ""
        shape(1) = "func=text;x=0;y=0;fn=Verdana;fs=16;fb=True;text=bold;bc=#000000;"
        shape(2) = "func=text;x=0;y=20;fn=Verdana;fs=16;fi=True;text=italics;bc=#000000;"
        shape(3) = "func=text;x=0;y=43;fn=Verdana;fs=32;fb=True;text=Heading 1;bc=#000000;"
        shape(4) = "func=text;x=0;y=84;fn=Verdana;fs=24;fb=True;text=Heading 2;bc=#000000;"
        shape(5) = "func=text;x=0;y=115;fn=Verdana;fs=20;fb=True;text=Heading 3;bc=#000000;"
        shape(6) = "func=text;x=0;y=140;fn=Verdana;fs=16;fb=True;text=Heading 4;bc=#000000;"
        shape(7) = "func=ell;x=20;y=167;x2=50;y2=54;width=5;height=5;bc=#000000;pw=0;"
        shape(8) = "func=text;x=30;y=160;fn=Verdana;fs=16;text=Bullet List;bc=#000000;"
        shape(9) = "func=ell;x=40;y=187;x2=50;y2=54;width=5;height=5;bc=#FFFFFF;pc=#000000;pw=1;"
        shape(10) = "func=text;x=50;y=180;fn=Verdana;fs=16;text=Bullet List 2;bc=#000000;"
        shape(11) = "func=text;x=18;y=200;fn=Verdana;fs=16;text=1. Number List;bc=#000000;"
        shape(12) = "func=text;x=38;y=220;fn=Verdana;fs=16;text=i. Number List 2;bc=#000000;"
        shape(13) = "func=img;x=0;y=238;src=http://www.nonkit.com/smallbasic.files/Turtle.png;"
        shape(14) = "func=rect;x=0;y=490;width=298;height=60;bc=#FFFFFF;pc=#666666;pw=1;"
        shape(15) = "func=rect;x=0;y=510;width=298;height=1;pc=#666666;pw=1;"
        shape(16) = "func=rect;x=0;y=530;width=298;height=1;pc=#666666;pw=1;"
        shape(17) = "func=rect;x=149;y=490;width=1;height=60;pc=#666666;pw=1;"
        shape(18) = "func=text;x=2;y=490;fn=Verdana;fs=16;fb=True;text=Table Heading 1;bc=#000000;"
        shape(19) = "func=text;x=151;y=490;fn=Verdana;fs=16;fb=True;text=Table Heading 2;bc=#000000;"
        shape(20) = "func=text;x=2;y=510;fn=Verdana;fs=16;text=Row 1-Cell 1;bc=#000000;"
        shape(21) = "func=text;x=151;y=510;fn=Verdana;fs=16;text=Row 1-Cell 2;bc=#000000;"
        shape(22) = "func=text;x=2;y=530;fn=Verdana;fs=16;text=Row 2-Cell 1;bc=#000000;"
        shape(23) = "func=text;x=151;y=530;fn=Verdana;fs=16;text=Row 2-Cell 2;bc=#000000;"
        shape(24) = "func=rect;x=0;y=554;width=578;height=1;pc=#666666;pw=1;"
    End Sub
    Sub Shapes_MoveToEntry()
        shp("x") = Microsoft.SmallBasic.Library.Math.Floor(x * 100) / 100
        shp("y") = Microsoft.SmallBasic.Library.Math.Floor(y * 100) / 100
    End Sub
    Sub Shapes_PenToEntry()
        shp("pw") = pw
        If 0 < pw Then
            shp("pc") = pc
        End If
    End Sub
    Sub Shapes_Restore()
        shape = _shape
        shX = _shX
        shY = _shY
    End Sub
    Sub Shapes_RotateToEntry()
        If angle <> 0 Then
            shp("angle") = Microsoft.SmallBasic.Library.Math.Floor(angle * 100) / 100
        End If
    End Sub
    Sub Shapes_Save()
        _shape = shape
        _shX = shX
        _shY = shY
    End Sub
End Module
