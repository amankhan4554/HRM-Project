
$(document).ready(function () {
    $("#Showbtn").mouseenter(function () {
        $("#Password").attr("type", "text");
    });

    $("#Showbtn").mouseleave("mouseleave", function () {
        $("#Password").attr("type", "password");
    });
   });
