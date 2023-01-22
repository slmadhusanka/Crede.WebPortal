$.fn.dataTable.ext.errMode = function (settings, tn, msg) {
    if (settings && settings.jqXHR && settings.jqXHR.status == 401) {
        window.location = window.location.origin + '/Login.aspx?ReturnUrl=' + encodeURIComponent(window.location.pathname);
        return false;
        //return; // Status code specific error handler will take care of this
    }
    console.error(msg); // Alert for all other error types
    //window.location = window.location.origin + '/Login.aspx?ReturnUrl=' + encodeURIComponent(window.location.pathname);
    return false;
};