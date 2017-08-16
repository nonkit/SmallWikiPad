$vbproj = "Graduate\SmallWikiPad.vbproj"
$data = Get-Content -Path $vbproj
for ($i = 0; $i -lt $data.Count; $i++) {
    if (0 -lt $data[$i].IndexOf("`"3.5`"")) { $data[$i] = $data[$i].Replace("`"3.5`"", "`"15.0`"") }
    if (0 -lt $data[$i].IndexOf("v3.5")) { $data[$i] = $data[$i].Replace("v3.5", "v4.5") }
    if (0 -lt $data[$i].IndexOf("\ (x86)")) { $data[$i] = $data[$i].Replace("\ (x86)", "") }
}
Write-Output $data | Set-Content -Path $vbproj -Encoding UTF8
   