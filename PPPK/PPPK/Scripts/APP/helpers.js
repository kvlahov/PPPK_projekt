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

function showInsertForm(getFormUrl, insertUrl, onSuccess = () => { }, onFormShow = () => { }) {

    $.ajax({
        type: 'GET',
        url: getFormUrl,
        dataType: 'html',
        success: function (response) {
            $('.modal-title').text('Create');
            $('.modal-body').html(response);
            $('.modal-footer').find('.btn-primary').remove();

            $('<button/>')
                .text('Create')
                .addClass('btn btn-primary')
                .on('click', e => {
                    if ($(e.target).closest('.modal-content').find('form').valid()) {
                        makeAjaxRequest(insertUrl, 'POST', { model: $('.modal-body').find('form').serializeObject() }, onSuccess);
                        $('#modal-form').modal('hide');
                    }
                })
                .appendTo($('.modal-footer'));

            $('#modal-form').modal('show');
            onFormShow();
            $.validator.unobtrusive.parse($('.modal-body'));
        }
    })
}

function showUpdateForm(getUrl, id, updateUrl, onSuccess = () => { }) {
    $.ajax({
        type: 'GET',
        url: getUrl,
        data: { id: id },
        dataType: 'html',
        success: function (response) {
            $('.modal-title').text('Update');
            $('.modal-body').html(response);
            $('.modal-footer').find('.btn-primary').remove();

            $('<button/>')
                .text('Update')
                .addClass('btn btn-primary')
                .on('click', e => {
                    makeAjaxRequest(updateUrl, 'PATCH', {
                        model: $('.modal-body').find('form').serializeObject()
                    }, onSuccess);

                    $('#modal-form').modal('hide');
                })
                .appendTo($('.modal-footer'));

            $('#modal-form').modal('show');
            //$.validator.unobtrusive.parse($('.modal-body'));
        }
    })
}

function deleteEntity(deleteUrl, id, onSuccess = () => { }) {
    alertify.confirm("Delete", "Are you sure you want to delete this record?", () => {
        makeAjaxRequest(deleteUrl, 'delete', { id: id }, onSuccess)
    }, () => { });
}

function makeAjaxRequest(url, method, data, onSuccess = () => { }) {
    $.ajax({
        type: method,
        url: url,
        contentType: 'application/json',
        data: JSON.stringify(data),
        dataType: 'json',
        success: function (response) {
            alertify.set('notifier', 'position', 'top-center');
            if (response.success) {
                alertify.success('Operation was successful');
                onSuccess();
            } else {
                alertify.error('Operation has failed!');
                response.errors.forEach(e => alertify.error(e));
            }
        }
    });
}