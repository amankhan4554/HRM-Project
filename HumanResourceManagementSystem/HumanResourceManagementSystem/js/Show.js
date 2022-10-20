
$(document).ready(function () {
    $("#Password").mouseenter(function () {
        $("#Password").attr("type", "text");
    });

    $("#Password").mouseleave("mouseleave", function () {
        $("#Password").attr("type", "password");
    });
   });
