var filterindex = 0;
$(document).ready(function () {
   
    $(".glyphicon-filter").click(function (e) {
        filterindex = $(event.target).closest('th').index();

        $("#dialog #filterclause").val($("th:eq(" + filterindex + ")").data("filterclause"));
        $("#dialog #FirstBox").val($("th:eq(" + filterindex + ")").data("filtervalue"));
        $("#dialog").dialog({

            position: { at: "right bottom", my: "left top", of: $(e.target) }
        });
        $("#dialog").dialog("open");
    });
    $("#close").click(function () {

        $("#dialog").dialog("close");
    });

    $("#ApplyFilter").click(function (e) {

        e.preventDefault();

        $("th:eq(" + filterindex + ")").data("filterclause", $("#filterclause").find("option:selected").text());
        $("th:eq(" + filterindex + ")").data("filtervalue", $("#FirstBox").val());
        FilterAddressTable();
    });


    function FilterAddressTable() {

        $("#AddressTable tr").each(function () {

            $(this).show();
        });

        $("#AddressTable th").each(function () {

            var headerindex = $(this).index();


            $(this).closest("table").find("tr:has(td):visible").each(function () {

                if (!$("th:eq(" + headerindex + ")").data("filtervalue")) {
                    $("th:eq(" + headerindex + ")").find("span:has(i.glyphicon.glyphicon-filter)").find("i.glyphicon.glyphicon-filter").css("visibility", "hidden");
                }
                else {

                    $("th:eq(" + headerindex + ")").find("span:has(i.glyphicon.glyphicon-filter)").find("i.glyphicon.glyphicon-filter").css("visibility", "visible");

                    switch ($("th:eq(" + headerindex + ")").data("filterclause")) {

                        case "Equals":

                            if ($(this).find("td:eq(" + headerindex + ")").text() === $("th:eq(" + headerindex + ")").data("filtervalue")) {
                                $(this).show();
                            }
                            else {
                                $(this).hide();
                            }
                            break;
                        case "Contains":

                            if ($(this).find("td:eq(" + headerindex + ")").is(":contains(" + $("th:eq(" + headerindex + ")").data("filtervalue") + ")")) {
                                $(this).show();
                            }
                            else {
                                $(this).hide();
                            } break;
                        case "Does not Contain":

                            if ($(this).find("td:eq(" + headerindex + ")").is(":not(:contains(" + $("th:eq(" + headerindex + ")").data("filtervalue") + "))")) {
                                $(this).show();
                            }
                            else {
                                $(this).hide();
                            } break;
                        case "Not Equal to": if ($(this).find("td:eq(" + headerindex + ")").text() != $("th:eq(" + headerindex + ")").data("filtervalue")) {
                            $(this).show();
                        }
                        else {
                            $(this).hide();
                        }
                            break;
                    }
                }
            });
        });
    }

    $("#ClearFilter").click(function (e) {

        e.preventDefault();

        $("th:eq(" + filterindex + ")").data("filterclause", "");
        $("th:eq(" + filterindex + ")").data("filtervalue", "");
        FilterAddressTable();
    });
});