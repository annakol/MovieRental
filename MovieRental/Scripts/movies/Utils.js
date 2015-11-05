function toJavaScriptDate(value) {
    var pattern = /Date\(([^)]+)\)/;
    var results = pattern.exec(value);
    var dt = new Date(parseFloat(results[1]));
    return dt.getFullYear() + (10 > (dt.getMonth() + 1) ? '-0' : '-') + (dt.getMonth() + 1) + "-" + (10 > dt.getDate() ? '0' : '') + dt.getDate();
}