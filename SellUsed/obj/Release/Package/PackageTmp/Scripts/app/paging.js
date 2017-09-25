$(document).ready(function () {
    $('#page_dropdown li').click ( function () {
        var size = parseInt($(this).text());
        $('#numberbox').html($(this).html());
        $('.page_1').attr('href', '"@Url.Action(Model.ViewName, "Products", new { pageSize = ' + size + ' })"');
        $('.page_2').attr('href', '"@Url.Action(Model.ViewName, "Products", new { page = Model.Pager.CurrentPage - 1, pageSize = ' + size + ' })"');
        $('.page_3').attr('href', '"@Url.Action(Model.ViewName, "Products", new { page = page, pageSize = ' + size + ' })"');
        $('.page_4').attr('href', '"@Url.Action(Model.ViewName, "Products", new { page = Model.Pager.CurrentPage + 1, pageSize = ' + size + ' })"');
        $('.page_5').attr('href', '"@Url.Action(Model.ViewName, "Products", new { page = Model.Pager.TotalPages, pageSize = ' + size + ' })"');
    });

});