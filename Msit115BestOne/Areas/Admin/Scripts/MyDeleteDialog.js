function showconfirm(Guid, ActionUrl) {
    var $dialog = $('<div id="dialog-confirm" ></div>')
           .html('<p><span class="ui-icon ui-icon-alert" style="float:left; margin:12px 12px 20px 0;"></span>刪除將無法回復，確定刪除嗎?</p>')
           .dialog({
               autoOpen: false,
               closeOnEscape: false,
               resizable: false,
               modal: true,
               width: '300',
               title: "刪除提醒視窗",
               buttons: {
                   "確認刪除": function () {
                       $(this).dialog("close");
                       //alert('啊啊啊啊啊啊~~~~~');
                       Delete(Guid, ActionUrl);
                   },
                   "取消": function () {
                       $(this).dialog("close");
                   }
               }
           });
    $dialog.dialog('open');
}

function Delete(Guid, ActionUrl) {
    $.ajax({
        type: 'POST',
        url: ActionUrl,
        //dataType: "json",
        data: {
            AdminID: Guid
        },
        success:
        function () {
            showDialog();
        },
        error: function () { showDialog('未知的錯誤!', false); }
    });
}

function showDialog() {
    var $dialog = $('<div id="dialog" ></div>')
           .html("資料刪除成功")
           .dialog({
               autoOpen: false,
               resizable: false,
               closeOnEscape: false,
               modal: true,
               width: '200',
               title: "訊息視窗",
               buttons: {
                   "確認": function () {
                       $(this).dialog("close");
                       location.reload();
                   }
               }
           });
    $dialog.dialog('open');
}