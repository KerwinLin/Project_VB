/*列印功能 */
function printScreen(printlist) {
    var value = printlist.innerHTML;
    var printPage = window.open("", "列印中，請稍後...", "");
    var link = "<link href=" + "../Styles/print.css" + " />";
    printPage.document.open();
    printPage.document.write("<HTML><head>" + link +"</head><BODY onload='window.print();'>"); /*window.close()*/
    printPage.document.write(value);
    printPage.document.close("</BODY></HTML>");

}

/*頁籤功能*/
$(function () {
    $("#tabs-nav a").click(function () {
        $("#tabs-nav a").removeClass("tabs-menu-active");
        $(this).addClass("tabs-menu-active");
        $(".tabs-panel").hide();
        var tab_id = $(this).attr("href");
        $(tab_id).fadeIn();
        return false;
    });
});