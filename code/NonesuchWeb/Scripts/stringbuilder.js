function StringBuilder(value) {
    this.strings = new Array("");
    this.append(value);
}

// Appends the given value to the end of this instance.
StringBuilder.prototype.append = function (value) {
    if (value) {
        this.strings.push(value);
    }
    return this;
}

// Appends the given format value to the end of this instance.
StringBuilder.prototype.appendFormat = function () {
    var arguments = StringBuilder.prototype.appendFormat.arguments;

    if (arguments.length > 0) {
        var value = arguments[0];
        for (var i = 1; i < arguments.length; ++i) {
            value = value.replace("{" + (i - 1) + "}", arguments[i]);
        }
        this.strings.push(value);
    }
    return this;
}


// Clears the string buffer
StringBuilder.prototype.clear = function () {
    this.strings.length = 1;
}

// Converts this instance to a String.
StringBuilder.prototype.toString = function () {
    return this.strings.join("");
}

//appendPrintf
StringBuilder.prototype.appendPrintf = function () {
    var arguments = StringBuilder.prototype.appendPrintf.arguments;
    if (!arguments || arguments.length < 1 || !RegExp) {
        Error('Error at StringBuilder.prototype.appendPrintf');
        return this;
    }

    var str = arguments[0];
    var formats = {
        '%': function (val) { return '%'; },
        'b': function (val) { return parseInt(val, 10).toString(2); },
        'c': function (val) { return String.fromCharCode(parseInt(val, 10)); },
        'd': function (val) { return parseInt(val, 10) ? parseInt(val, 10) : 0; },
        'u': function (val) { return Math.abs(val); },
        'f': function (val, p) { return (p > -1) ? Math.round(parseFloat(val) * Math.pow(10, p)) / Math.pow(10, p) : parseFloat(val); },
        'o': function (val) { return parseInt(val, 10).toString(8); },
        's': function (val) { return val; },
        'x': function (val) { return ('' + parseInt(val, 10).toString(16)).toLowerCase(); },
        'X': function (val) { return ('' + parseInt(val, 10).toString(16)).toUpperCase(); }
    };

    var re = /([^%]*)%('.|0|\x20)?(-)?(\d+)?(\.\d+)?(%|b|c|d|u|f|o|s|x|X)(.*)/;
    var a = [], numSubstitutions = 0;

    while (a = re.exec(str)) {
        var leftpart = a[1], pPad = a[2], pJustify = a[3], pMinLength = a[4];
        var pPrecision = a[5], pType = a[6], rightPart = a[7];

        numSubstitutions++;
        if (numSubstitutions >= arguments.length) {
            Error('Error! Not enough function arguments (' + (arguments.length - 1) + ', excluding the string)\nfor the number of substitution parameters in string (' + numSubstitutions + ' so far).');
            return this;
        }
        var param = arguments[numSubstitutions];
        var subst = param;
        var formatsFunc = formats[pType];

        if (formatsFunc != null) {
            if (pType == 'f') {
                var precision = (pPrecision && pType == 'f') ? parseInt(pPrecision.substring(1)) : -1;
                subst = formatsFunc(param, precision);
            } else {
                subst = formatsFunc(param);
            }
        }
        if (leftpart) {
            this.strings.push(leftpart);
        }
        if (subst) {
            this.strings.push(subst);
        }
        str = rightPart;
    }
    if (str) {
        this.strings.push(str);
    }
    return this;
}