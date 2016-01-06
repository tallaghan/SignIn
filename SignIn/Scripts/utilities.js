function ConvertDotNetDateToString(dt) {

    var m = moment(dt);

    return m.format('ddd, DD MMM YYYY HH:mm:ss');
}