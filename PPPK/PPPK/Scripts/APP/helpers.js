function getHtmlDateForDataTable(dateJson) {
    const dateLocale = "nb-NO";
    if (dateJson == null) {
        return "";
    }

    const date = new Date(parseInt(dateJson.substr(6)));
    const yyyyMMdd = date.toISOString().replace(/-/g, '').split('T')[0];

    const spanTag =
        $('<span/>')
            .text(yyyyMMdd)
            .hide();

    const container =
        $('<div/>')
            .append(spanTag)
            .append(date.toLocaleDateString(dateLocale));

    return container.html();
}

$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        if (o[this.name]) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
};

function getSelectedItemId(table) {
    const data = table.rows({ selected: true }).data()[0];
    if (!data) {
        return -1;
    }
    const idKey = Object.keys(data)[0];
    return data[idKey];
}