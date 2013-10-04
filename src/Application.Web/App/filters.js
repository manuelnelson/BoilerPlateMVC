TodoApp.filter('dollarAmount', function () {
    return function (input) {
        var isNegative = input < 0;
        var dollarInput = Math.abs(input).toFixed(2);
        return isNegative ? '-$' + dollarInput : '$' + dollarInput;
    };
});
TodoApp.filter('percentage', function () {
    return function (input) {
        var percent = Math.abs(input).toFixed(2);
        return percent + '%';
    };
});