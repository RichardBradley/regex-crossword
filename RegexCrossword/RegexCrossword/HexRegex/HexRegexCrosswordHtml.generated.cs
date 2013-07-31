﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18034
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RegexCrossword.HexRegex
{
    
    #line 2 "..\..\HexRegex\HexRegexCrosswordHtml.cshtml"
    using System;
    
    #line default
    #line hidden
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    #line 4 "..\..\HexRegex\HexRegexCrosswordHtml.cshtml"
    using System.Web.Script.Serialization;
    
    #line default
    #line hidden
    
    #line 3 "..\..\HexRegex\HexRegexCrosswordHtml.cshtml"
    using HexRegex;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "1.5.4.0")]
    internal partial class HexRegexCrosswordHtml : RazorGenerator.Templating.RazorTemplateBase
    {
#line hidden

        #line 5 "..\..\HexRegex\HexRegexCrosswordHtml.cshtml"

    public HexRegexCrossword Model { get; set; }

        #line default
        #line hidden

        public override void Execute()
        {


WriteLiteral("\r\n");





WriteLiteral("\r\n<html>\r\n    <head>\r\n        <style type=\"text/css\">\r\n            \r\n            " +
"body {\r\n                overflow-x: hidden;\r\n            }\r\n\r\n            #conta" +
"iner {\r\n                position: relative;\r\n            }\r\n\r\n            .hexag" +
"on, .clue {\r\n                display: block;\r\n                padding: 0;\r\n     " +
"           height: 2em;\r\n                line-height: 2em;\r\n                posi" +
"tion: absolute;\r\n            }\r\n            \r\n            .hexagon {\r\n          " +
"      text-align: center;\r\n                overflow: hidden;\r\n                fo" +
"nt-size: 16px;\r\n            }\r\n\r\n            .clue {\r\n                text-align" +
": right;\r\n                width: 20em;\r\n                padding-right: 20em;\r\n  " +
"              font-size: 18px;\r\n            }\r\n\r\n            .clue.x {\r\n        " +
"        transform: rotate(240deg);\r\n                -webkit-transform: rotate(24" +
"0deg);\r\n            }\r\n\r\n            .clue.y {\r\n                transform: rotat" +
"e(120deg);\r\n                -webkit-transform: rotate(120deg);\r\n            }\r\n\r" +
"\n            .clue.current span {\r\n                background-color: #b3d4fc;\r\n " +
"           }\r\n\r\n            .clue.current.changed span {\r\n                backgr" +
"ound-color: #25649F;\r\n                color: white;\r\n            }\r\n\r\n          " +
"  #controls {\r\n                position: absolute;\r\n            }\r\n        </sty" +
"le>\r\n        <script type=\"text/javascript\" src=\"file:///C:/Documents%20and%20Se" +
"ttings/rtb/Documents/personal/src/regex-crossword/RegexCrossword/RegexCrossword/" +
"bin/Debug/lib/jquery-1.6.2.min.js\"></script>\r\n        <script type=\"text/javascr" +
"ipt\" src=\"file:///C:/Documents%20and%20Settings/rtb/Documents/personal/src/regex" +
"-crossword/RegexCrossword/RegexCrossword/bin/Debug/lib/jquery.quickfit.js\"></scr" +
"ipt>\r\n        <script type=\"text/javascript\">\r\n            // use axial coords h" +
"ttp://www.redblobgames.com/grids/hexagons/\r\n            // hexes involved are th" +
"ose for which x,y,z all in [-6,6]\r\n            // always have x + y + z = 0\r\n\r\n " +
"           var solveLog = ");


            
            #line 71 "..\..\HexRegex\HexRegexCrosswordHtml.cshtml"
                       Write(new JavaScriptSerializer().Serialize(Model.SolveLog));

            
            #line default
            #line hidden
WriteLiteral(";\r\n            var solveLogIdx = -1; // just before the start\r\n\r\n");


            
            #line 74 "..\..\HexRegex\HexRegexCrosswordHtml.cshtml"
               var firstX = true; 

            
            #line default
            #line hidden
WriteLiteral("            var hexDataByQR = {\r\n");


            
            #line 76 "..\..\HexRegex\HexRegexCrosswordHtml.cshtml"
             for (int x = -6; x <= 6; x++)
            {

            
            #line default
            #line hidden
WriteLiteral("                ");


            
            #line 78 "..\..\HexRegex\HexRegexCrosswordHtml.cshtml"
                  Write(firstX ? "" : ",");

            
            #line default
            #line hidden
WriteLiteral(" \"");


            
            #line 78 "..\..\HexRegex\HexRegexCrosswordHtml.cshtml"
                                       Write(x);

            
            #line default
            #line hidden
WriteLiteral("\":{");

WriteLiteral("\r\n");


            
            #line 79 "..\..\HexRegex\HexRegexCrosswordHtml.cshtml"
                firstX = false;
                var firstZ = true;
                for (int y = -6; y <= 6; y++)
                {
                    var z = 0 - x - y;
                    if (Math.Abs(z) <= 6)
                    {

            
            #line default
            #line hidden
WriteLiteral("                        ");


            
            #line 86 "..\..\HexRegex\HexRegexCrosswordHtml.cshtml"
                          Write(firstZ ? "" : ",");

            
            #line default
            #line hidden
WriteLiteral(" \"");


            
            #line 86 "..\..\HexRegex\HexRegexCrosswordHtml.cshtml"
                                               Write(z);

            
            #line default
            #line hidden
WriteLiteral("\" : \"");


            
            #line 86 "..\..\HexRegex\HexRegexCrosswordHtml.cshtml"
                                                      Write(x);

            
            #line default
            #line hidden
WriteLiteral(",");


            
            #line 86 "..\..\HexRegex\HexRegexCrosswordHtml.cshtml"
                                                         Write(z);

            
            #line default
            #line hidden
WriteLiteral("\"");

WriteLiteral("\r\n");


            
            #line 87 "..\..\HexRegex\HexRegexCrosswordHtml.cshtml"
                        firstZ = false;
                    }
                }

            
            #line default
            #line hidden
WriteLiteral("                ");

WriteLiteral("}");

WriteLiteral("\r\n");


            
            #line 91 "..\..\HexRegex\HexRegexCrosswordHtml.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("            };\r\n            var cluesByAxisIdx = {\r\n");


            
            #line 94 "..\..\HexRegex\HexRegexCrosswordHtml.cshtml"
                 for (var axis = 'X'; axis <= 'Z'; axis++) {

            
            #line default
            #line hidden
WriteLiteral("                    ");


            
            #line 95 "..\..\HexRegex\HexRegexCrosswordHtml.cshtml"
                      Write(axis == 'X' ? "" : ",");

            
            #line default
            #line hidden
WriteLiteral("\"");


            
            #line 95 "..\..\HexRegex\HexRegexCrosswordHtml.cshtml"
                                               Write(axis);

            
            #line default
            #line hidden
WriteLiteral("\":{");

WriteLiteral("\r\n");


            
            #line 96 "..\..\HexRegex\HexRegexCrosswordHtml.cshtml"
                    for (var idx = -6; idx <= 6; idx++) {

            
            #line default
            #line hidden
WriteLiteral("                         ");


            
            #line 97 "..\..\HexRegex\HexRegexCrosswordHtml.cshtml"
                           Write(idx == -6 ? "" : ",");

            
            #line default
            #line hidden
WriteLiteral("\"");


            
            #line 97 "..\..\HexRegex\HexRegexCrosswordHtml.cshtml"
                                                  Write(idx);

            
            #line default
            #line hidden
WriteLiteral("\":\"");


            
            #line 97 "..\..\HexRegex\HexRegexCrosswordHtml.cshtml"
                                                         Write(Model.Clues[axis - 'X',idx + 6]);

            
            #line default
            #line hidden
WriteLiteral("\"");

WriteLiteral("\r\n");


            
            #line 98 "..\..\HexRegex\HexRegexCrosswordHtml.cshtml"
                    }

            
            #line default
            #line hidden
WriteLiteral("                    ");

WriteLiteral("}");

WriteLiteral("\r\n");


            
            #line 100 "..\..\HexRegex\HexRegexCrosswordHtml.cshtml"
                }

            
            #line default
            #line hidden
WriteLiteral("            };\r\n\r\n            var SQRT_3 = Math.sqrt(3);\r\n            // SIZE is " +
"the edge length of a single hex\r\n            var SIZE = 25;\r\n            var CEN" +
"TRE_X = SIZE * 18;\r\n            var CENTRE_Y = CENTRE_X;\r\n\r\n            var hexC" +
"ornerOffsets = [];\r\n            for (var i = 0; i <= 6; i++) {\r\n                " +
"var angle = 2 * Math.PI / 6 * (i + 0.5);\r\n                var x_i = SIZE * Math." +
"cos(angle);\r\n                var y_i = SIZE * Math.sin(angle);\r\n                " +
"hexCornerOffsets[i] = { \'x\': x_i, \'y\': y_i };\r\n            }\r\n\r\n            func" +
"tion hexQRtoPixelXY(q, r) {\r\n                var x = CENTRE_X + (SIZE * SQRT_3 *" +
" (q + r / 2.0));\r\n                var y = CENTRE_Y + (SIZE * 3.0 / 2.0 * r);\r\n  " +
"              return { \"x\": x, \"y\": y };\r\n            }\r\n            \r\n         " +
"   // See HexRegexCrossword.AxisIdxOffsetToQR\r\n            function axisIdxOffse" +
"tToQR(axis, idx, offset) {\r\n                var q, r;\r\n                if (axis " +
"== \'X\') {\r\n                    q = idx;\r\n                    r = Math.min(6, 6 -" +
" idx);\r\n                    r -= offset;\r\n                } else if (axis == \'Y\'" +
") {\r\n                    q = Math.min(6, 6 - idx);\r\n                    r = Math" +
".max(-6, - 6 - idx);\r\n                    q -= offset;\r\n                    r +=" +
" offset;\r\n                } else { // axis == \'Z\'\r\n                    q = Math." +
"max(-6, -6 - idx);\r\n                    r = idx;\r\n                    q += offse" +
"t;\r\n                }\r\n                return { q: q, r: r };\r\n            }\r\n\r\n" +
"            function historyBack() {\r\n                if (solveLogIdx <= 0) {\r\n " +
"                   alert(\'Already at the start\');\r\n                } else {\r\n   " +
"                 revertStep(solveLog[solveLogIdx]);\r\n                    showSte" +
"p(solveLog[--solveLogIdx]);\r\n                }\r\n            }\r\n\r\n            fun" +
"ction historyForward() {\r\n                if (solveLogIdx + 1 >= solveLog.length" +
") {\r\n                    alert(\'Already at the end\');\r\n                } else {\r" +
"\n                    showStep(solveLog[++solveLogIdx]);\r\n                }\r\n    " +
"        }\r\n\r\n            $(document).keypress(function(event) {\r\n               " +
" if (event.which == 39) {\r\n                    event.preventDefault();\r\n        " +
"            historyForward();\r\n                } else if (event.which == 37) {\r\n" +
"                    event.preventDefault();\r\n                    historyBack();\r" +
"\n                }\r\n            });\r\n\r\n            function showStep(step) {\r\n  " +
"              console.log(step);\r\n\r\n                $(\'.clue\').removeClass(\'curr" +
"ent changed\');\r\n\r\n                // {\"InspectedAxis\":\"X\",\"InspectedIdx\":-6,\"New" +
"RowValue\":\".......\",\"Changed\":false}\r\n                var clue = cluesByAxisIdx[" +
"step.InspectedAxis][step.InspectedIdx];\r\n                clue.addClass(\'current\'" +
");\r\n                if (step.Changed) clue.addClass(\'changed\');\r\n\r\n             " +
"   var cells = step.NewRowValue.match(/\\.|\\[[^\\]]*\\]|[A-Z]/g);\r\n\r\n              " +
"  // store the old cells, if necessary, to support the back button:\r\n           " +
"     var storeOldValues = false;\r\n                if (!step.OldRowValues) {\r\n   " +
"                 step.OldRowValues = [];\r\n                    storeOldValues = t" +
"rue;\r\n                }\r\n                \r\n                // now iterate along " +
"the clue axis and update the letter\r\n                for (var offset = 0; offset" +
" < cells.length; offset++) {\r\n                    var qr = axisIdxOffsetToQR(ste" +
"p.InspectedAxis, step.InspectedIdx, offset);\r\n                    var cell = hex" +
"DataByQR[qr.q][qr.r];\r\n\r\n                    var oldVal = cell.text();\r\n        " +
"            var newVal = cells[offset];\r\n\r\n                    if (storeOldValue" +
"s) {\r\n                        step.OldRowValues[offset] = oldVal;\r\n             " +
"       }\r\n\r\n                    if (oldVal !== newVal) {\r\n                      " +
"  console.log(\"Cell q = \" + qr.q + \", r = \" + qr.r + \" changed from \'\" +\r\n      " +
"                      oldVal + \"\' to \'\" + newVal + \"\'\");\r\n\r\n                    " +
"    cell.text(newVal);\r\n                        cell.quickfit();\r\n              " +
"      }\r\n                }\r\n            }\r\n\r\n            function revertStep(ste" +
"p) {\r\n                for (var offset = 0; offset < step.OldRowValues.length; of" +
"fset++) {\r\n                    var qr = axisIdxOffsetToQR(step.InspectedAxis, st" +
"ep.InspectedIdx, offset);\r\n                    var cell = hexDataByQR[qr.q][qr.r" +
"];\r\n                    var newVal = step.OldRowValues[offset];\r\n               " +
"     if (cell.text() !== newVal) {\r\n                        cell.text(newVal);\r\n" +
"                        cell.quickfit();\r\n                    }\r\n               " +
" }\r\n            }\r\n\r\n            // The following code sets up the UI\r\n         " +
"   $(function () {\r\n                var container = $(\'#container\');\r\n          " +
"      var canvas = document.getElementById(\"canvas\").getContext(\'2d\');\r\n        " +
"        $.each(hexDataByQR, function (q, rs) {\r\n                    q = q - 0;\r\n" +
"                    $.each(rs, function (r, val) {\r\n                        r = " +
"r - 0;\r\n                        var xy = hexQRtoPixelXY(q, r);\r\n\r\n              " +
"          var div = $(\"<div />\")\r\n                            .addClass(\'hexagon" +
"\')\r\n                            .attr(\'title\', val);\r\n                        co" +
"ntainer.append(div);\r\n                        div.width(SQRT_3 * SIZE);\r\n       " +
"                 div.css({ top: xy.y - div.height() / 2, left: xy.x - div.width(" +
") / 2 });\r\n                        hexDataByQR[q][r] = div;\r\n\r\n                 " +
"       canvas.beginPath();\r\n                        canvas.moveTo(xy.x + hexCorn" +
"erOffsets[0].x, xy.y + hexCornerOffsets[0].y);\r\n                        for (var" +
" i = 1; i <= 6; i++) {\r\n                            canvas.lineTo(xy.x + hexCorn" +
"erOffsets[i].x, xy.y + hexCornerOffsets[i].y);\r\n                        }\r\n     " +
"                   canvas.stroke();\r\n                    });\r\n                })" +
";\r\n                $.each(cluesByAxisIdx, function (axis, cluesByIdx) {\r\n       " +
"             $.each(cluesByIdx, function (idx, clue) {\r\n                        " +
"idx = idx - 0;\r\n                        var div = $(\"<div />\")\r\n                " +
"            .addClass(\"clue\")\r\n                            .addClass(axis)\r\n    " +
"                        .append($(\'<span />\').text(clue))\r\n                     " +
"   container.append(div);\r\n\r\n                        // if axis == x\r\n          " +
"              // x,y,z here are hex cube coords\r\n                        var x =" +
" idx;\r\n                        var y = Math.max(-6, - 6 - x) - 0.7;  // min y fo" +
"r x == idx, dec 0.7 to move out of grid\r\n                        var z = - x - y" +
"; // fixed\r\n\r\n                        var tmp;\r\n                        if (axis" +
" == \'Y\') {\r\n                            tmp = y;\r\n                            y " +
"= x;\r\n                            x = z;\r\n                            z = tmp;\r\n" +
"                        } else if (axis == \'Z\') {\r\n                            t" +
"mp = z;\r\n                            z = x;\r\n                            x = y;\r" +
"\n                            y = tmp;\r\n                        }\r\n\r\n            " +
"            // xy is pixel coords\r\n                        var xy = hexQRtoPixel" +
"XY(x, z);\r\n\r\n                        div.css({ top: xy.y - div.height() / 2, lef" +
"t: xy.x - div.width() });\r\n                        cluesByAxisIdx[axis][idx] = d" +
"iv;\r\n                    });\r\n                });\r\n                \r\n           " +
"     var xy = hexQRtoPixelXY(-6, 9);\r\n                var controls = $(\'#control" +
"s\');\r\n                controls.css({ top: xy.y, left: xy.x - controls.width() })" +
";\r\n            });\r\n        </script>\r\n    </head>\r\n    <body>\r\n        <div id=" +
"\"container\">\r\n            <canvas id=\"canvas\" width=\"1000\" height=\"1000\"></canva" +
"s>\r\n            <div id=\"elts\"></div>\r\n            <div id=\"controls\">\r\n        " +
"        <button onClick=\"historyBack()\" title=\"Go one step back\">&#x21E6;</butto" +
"n>\r\n                <button onClick=\"historyForward()\" title=\"Go one step forwar" +
"d\">&#x21E8;</button>\r\n            </div>\r\n        </div>\r\n    </body>\r\n</html>\r\n" +
"");


        }
    }
}
#pragma warning restore 1591
