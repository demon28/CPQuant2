var calcu = {};
//加法
calcu.add = function (a, b) {
    var c, d, e;
    try {
        c = a.toString().split(".")[1].length;
    } catch (f) {
        c = 0;
    }
    try {
        d = b.toString().split(".")[1].length;
    } catch (f) {
        d = 0;
    }
    e = Math.pow(10, Math.max(c, d));
    return (calcu.mul(a, e) + calcu.mul(b, e)) / e;
}
//减法
calcu.sub = function (a, b) {
    var c, d, e;
    try {
        c = a.toString().split(".")[1].length;
    } catch (f) {
        c = 0;
    }
    try {
        d = b.toString().split(".")[1].length;
    } catch (f) {
        d = 0;
    }
    e = Math.pow(10, Math.max(c, d));
    return (calcu.mul(a, e) - calcu.mul(b, e)) / e;
}
//乘法
calcu.mul = function (a, b) {
    var c = 0,
        d = a.toString(),
        e = b.toString();
    try {
        c += d.split(".")[1].length;
    } catch (f) { }
    try {
        c += e.split(".")[1].length;
    } catch (f) { }
    return Number(d.replace(".", "")) * Number(e.replace(".", "")) / Math.pow(10, c);
}
//除法
calcu.div = function (a, b) {
    var c, d, e = 0,
        f = 0;
    try {
        e = a.toString().split(".")[1].length;
    } catch (g) { }
    try {
        f = b.toString().split(".")[1].length;
    } catch (g) { }
    c = Number(a.toString().replace(".", ""));
    d = Number(b.toString().replace(".", ""));
    var v = c / d * Math.pow(10, f - e);
    return calcu.toDecimal(v, 2);
}

calcu.toDecimal = function (number, f) {
    if (isNaN(number)) return 0;
    var vv = Math.pow(10, f);
    return Math.round(number * vv) / vv;
}