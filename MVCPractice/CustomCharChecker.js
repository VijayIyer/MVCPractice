(function ($) {
    $.validator.unobtrusive.adapters.addSingleVal("custom", "inputstring");
    $.validator.addMethod("custom", function (value, element, custom) {
        return !String(value).includes("*");
        
    })
})(jQuery);