/// <reference path="../jquery/jquery-1.10.2.min.js" />
/// <reference path="../jquery-easyui/jquery.easyui.min.js" />

var FrameHelper = {}

FrameHelper.TabPage =
{
    createIFrame: function (iframeID, iframeUrl) {
        var $iframeWrap = $("<div id=\"" + iframeID + "Wrap\" style=\"position:relative;height:100%\"></div>");
        var $iframeLoad = $("<div class=\"" + iframeID + "Load\" style=\"position:absolute;top:10px;left:10px;\"><i class=\"fonticon-spin fonticon-refresh\"></i> 正在加载...</div>");
        var $iframe = $("<iframe class=\"" + iframeID + "\" src=\"" + iframeUrl + "\" style=\"width:100%;height:100%;margin:auto;\" frameborder=\"0\" marginwidth=\"0\" marginheight=\"0\"></iframe>");

        $iframeWrap.append($iframeLoad);
        $iframeWrap.append($iframe);
        $iframe.load(function () {
            $iframeLoad.hide();
            $iframeLoad.remove();
        });

        return $iframeWrap;
    },
    Open: function (options) {
        var settings = {
            tabpanelID: "tabpanel",
            title: "未命名",
            url: "about:blank",
            iconCls: "glyphicon glyphicon-star-empty",
            closable: true,
            callback: null
        };

        settings = $.extend(settings, options);

        var $tab = $("#" + settings.tabpanelID);

        if ($tab.tabs("exists", settings.title)) {
            $tab.tabs("select", settings.title);
            return;
        }

        var $iframeWrap = FrameHelper.TabPage.createIFrame(settings.title, settings.url);
        var $iframeLoad = $("." + settings.title + "Load", $iframeWrap);
        var $iframe = $("." + settings.title, $iframeWrap);

        $tab.tabs("add",
        {
            title: settings.title,
            closable: settings.closable,
            iconCls: settings.iconCls,
            content: $iframeWrap
        });

        //$.data($iframe[0], "callback", settings.callback);
    },
    Close: function (options) {
        var settings = {
            tabpanelID: "tabpanel",
            title: "未命名"
        };

        settings = $.extend(settings, options);

        var $tab = $("#" + settings.tabpanelID);
        $tab.tabs("close", settings.title);
    },
    Callback: function (options) {
        var settings = {
            tabpanelID: "tabpanel",
            title: "未命名",
            result: null
        };

        settings = $.extend(settings, options);

        var $iframeWrap = $("#" + settings.title + "Wrap");
        var $iframe = $("." + settings.title, $iframeWrap);

        var callback = $.data($iframe[0], "callback");
        if (callback) { callback(options.result); }
    }
}

FrameHelper.Window =
{
    createIFrame: function (iframeID) {
        var $iframeWrap = $("<div id=\"" + iframeID + "Wrap\" style=\"position:relative;height:100%\"></div>");
        var $iframeLoad = $("<div id=\"" + iframeID + "Load\" style=\"position:absolute;top:10px;left:10px;\"><i class=\"fonticon-spin fonticon-refresh\"></i> 正在加载...</div>");
        var $iframe = $("<iframe id=\"" + iframeID + "Frame\" src=\"about:blank\" style=\"width:98%;height:98%;\" frameborder=\"0\" marginwidth=\"0\" marginheight=\"0\"></iframe>");

        $iframeWrap.append($iframeLoad);
        $iframeWrap.append($iframe);
        $iframe.load(function () {
            $iframeLoad.hide();
            $iframeLoad.remove();
        });

        return $iframeWrap;
    },
    Open: function (options) {
        var settings = {
            title: "未命名",
            url: "about:blank",
            width: 600,
            height: 400,
            iconCls: null,
            collapsible: true,
            minimizable: false,
            maximizable: true,
            closable: true,
            resizable: true,
            modal: true,
            callback: null
        };

        settings = $.extend(settings, options);

        //设置WindowID
        var $randomId = Math.floor(Math.random() * 999 + 1);
        var $iframeWrap = FrameHelper.Window.createIFrame("window_" + $randomId);
        var $iframeLoad = $("#window_" + $randomId + "Load", $iframeWrap);
        var $iframe = $("#window_" + $randomId + "Frame", $iframeWrap);
        $.data($iframe[0], "randomId", $randomId);
        $.data($iframe[0], "callback", settings.callback);

        var $window = $("<div id=\"window_" + $randomId + "\"></div>");
        $("body").append($window);

        //创建Window对象
        $window.window({
            title: settings.title,
            width: settings.width,
            height: settings.height,
            iconCls: settings.iconCls,
            minimizable: settings.minimizable,
            maximizable: settings.maximizable,
            closable: settings.closable,
            resizable: settings.resizable,
            modal: settings.modal,
            content: $iframeWrap,
            onClose: function () {
                $.removeData($iframe[0], "randomId");
                $.removeData($iframe[0], "callback");

                $iframe[0].contentWindow.document.write('');
                $iframe[0].contentWindow.close();
                $iframe.attr("src", "about:blank");
                $iframe.remove();

                $window.window("destroy");
                $window.remove();
            }
        });

        $iframe.attr("src", options.url);
        $window.window("open");
    },
    Close: function (options) {
        var settings = {
            iframeId: "0"
        };

        settings = $.extend(settings, options);

        var $iframe = $("#" + settings.iframeId);
        var $randomId = $.data($iframe[0], "randomId");
        var $window = $("#window_" + $randomId);
        $window.window("close");
    },
    Callback: function (options) {
        var settings = {
            iframeId: "0",
            result: null
        };

        settings = $.extend(settings, options);

        var $iframe = $("#" + settings.iframeId);
        var callback = $.data($iframe[0], "callback");
        if (callback) { callback(settings.result); }
    }
}

FrameHelper.Dialog =
{
    createHtml: function (options) {
        var html = "<div class=\"am-modal am-modal-prompt\" tabindex=\"-1\" id=\"" + options.id + "\">";
        html += "<div class=\"am-modal-dialog\">";
        html += "<div class=\"am-modal-hd\">" + options.title + "<a href=\"javascript: void(0)\" class=\"am-close am-close-spin\" data-am-modal-close>&times;</a></div>";
        html += "<div class=\"am-modal-bd\" style=\"padding:0px;background-color: white;\">";
        //加载元素
        html += "<div class=\"am-alert am-alert-warning\" data-am-alert style=\"position: absolute;width: 100%;\"><i class=\"am-icon-spinner am-icon-spin\"></i>" + options.loadText + "</div>";
        //iframe元素
        html += " <iframe id=\"" + options.id + "_iframe\" src=\"" + options.url + "\" style=\"width:100%; height:" + (options.height - 98) + "px;padding:0px;margin:0px;\"></iframe></div>";
        html += "<div class=\"am-modal-footer\"><span class=\"am-modal-btn\">取消</span> <span class=\"am-modal-btn\" data-am-modal-confirm>确定</span> </div> </div>";
        return html;
    },
    Open: function (options) {
        var settings = {
            width: 450,
            height: 300,
            title: "未命名",
            url: "about:blank",
            callback: null,
            closeViaDimmer: false,
            loadText: "正在拼命加载中...."
        };
        settings = $.extend(settings, options);
        settings.id = Math.floor(Math.random() * 999 + 1);
        var html = $(FrameHelper.Dialog.createHtml(settings));
        var iframe = $("iframe", html)[0];

        //加载控制
        var iframeLoad = $(".am-alert", html)[0];
        iframe.onload = function () {
            $(iframeLoad).alert('close');
        };

        $("body").append(html);
        //保存数据
        $.data(iframe, "randomId", settings.id);
        $.data(iframe, "callback", settings.callback);

        //当关闭后则销毁掉
        $('#' + settings.id).on('closed.modal.amui', function () {
            $(this).remove();
        });
        //打开模拟对话框
        $('#' + settings.id).modal({
            relatedTarget: this,
            closeOnConfirm: false,
            closeViaDimmer: settings.closeViaDimmer,
            width: settings.width,
            height: settings.height,
            onConfirm: function (e) {
                var _submit = $.data(iframe, "submit");
                if (_submit) { _submit(); }
            }
        });
    },
    Close: function (options) {
        var settings = {
            iframeId: "0"
        };

        settings = $.extend(settings, options);

        var $iframe = $("#" + settings.iframeId);
        var randomId = $.data($iframe[0], "randomId");
        $("#" + randomId).modal('close');
    },
    Submit: function (options) {
        var settings = {
            iframeId: "0",
            fn: null
        };

        settings = $.extend(settings, options);

        var $iframe = $("#" + settings.iframeId);
        $.data($iframe[0], "submit", settings.fn);
    },
    Callback: function (options) {
        var settings = {
            iframeId: "0",
            result: null
        };
        settings = $.extend(settings, options);

        var $iframe = $("#" + settings.iframeId);
        var callback = $.data($iframe[0], "callback");
        if (callback) { callback(settings.result); }
    },
}

FrameHelper.Message =
{
    ShowError: function (title, msg) {
        $.messager.alert(title, msg, "error");
    },
    ShowInfo: function (title, msg) {
        $.messager.alert(title, msg, "info");
    },
    ShowWarning: function (title, msg) {
        $.messager.alert(title, msg, "warning");
    },
    ShowConfirm: function (title, msg, fn) {
        $.messager.confirm(title, msg, fn);
    },
    ShowPopu: function (title, msg, timeout) {
        $.messager.show({ title: title, msg: msg, timeout: timeout, showType: 'slide' });
    }
}
