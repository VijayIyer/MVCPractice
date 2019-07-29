var filterindex = 0;
$(document).ready(function () {
   
    $(document).on('click', '.table .glyphicon-filter', function (e) {
        filterindex = $(event.target).closest('th').index();

        $("#dialog #filterclause").val($("th:eq(" + filterindex + ")").data("filterclause"));
        $("#dialog #FirstBox").val($("th:eq(" + filterindex + ")").data("filtervalue"));
        $("#dialog").dialog({

            position: { at: "right bottom", my: "left top", of: $(e.target) }
        });
        $("#dialog").dialog("open");
    });
    $("#dialog").on('click','#close',function () {

        $("#dialog").dialog("close");
    });

    $("#dialog").on('click', '#ApplyFilter', function (e) {

        e.preventDefault();

        $("th:eq(" + filterindex + ")").data("filterclause", $("#filterclause").find("option:selected").text());
        $("th:eq(" + filterindex + ")").data("filtervalue", $("#FirstBox").val());
        FilterAddressTable();
    });
    function FilterAddressTable() {

        $(".table tr").each(function () {

            $(this).show();
        });

        $(".table th").each(function () {

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
                        case "Not Equal to": if ($(this).find("td:eq(" + headerindex + ")").text() !== $("th:eq(" + headerindex + ")").data("filtervalue")) {
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

    $("#dialog").on('click', '#ClearFilter', function (e) {

        e.preventDefault();

        $("th:eq(" + filterindex + ")").data("filterclause", "");
        $("th:eq(" + filterindex + ")").data("filtervalue", "");
        FilterAddressTable();
    });
});