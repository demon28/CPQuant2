
function isShow(z) {

    if (z == "173" || z == "190" || z == "189" || z == "110" || z == "65" || z == "66" || z == "67" || z == "68" || z == "69" || z == "70" || z == "71" || z == "72" || z == "73" || z == "74" || z == "75" || z == "76" ||
        z == "77" || z == "78" || z == "79" || z == "80" || z == "81" || z == "82" || z == "83" || z == "84" || z == "85" || z == "86" || z == "87" || z == "88" || z == "89" || z == "90") {
        return true;
    } else {
        return false;
    }
}


function setCodeMirror() {

    var code = document.getElementById('code');
    var editor = CodeMirror.fromTextArea(code, {
        mode: "text/javascript",
        lineNumbers: true,
        theme: "eclipse",
        extraKeys: { "Ctrl-Q": "autocomplete" },
        completeSingle: false,
        lineWrapping: true,	//代码折叠
        foldGutter: true,
        matchBrackets: true,	//括号匹配
        gutters: ["CodeMirror-linenumbers", "CodeMirror-foldgutter"],

    });
    editor.on("keyup", function (cm, name, Event) {

        if (isShow(name.keyCode)) {
            editor.showHint({ completeSingle: false });
        }
    });
    editor.setSize("1200", "800");

    return editor;
}



function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}