$(document).ready(function () {

    var dt = new Date();
    var tzo = dt.getTimezoneOffset();
    document.cookie = "timeZoneOffset=" + tzo + "; path=/";

});