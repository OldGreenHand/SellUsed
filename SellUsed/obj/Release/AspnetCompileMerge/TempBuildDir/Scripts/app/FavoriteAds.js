$(document).ready(function () {

    $('.favorates-icon').on("click", function() {
        var id = $(this).attr("data-id");
        var $this = $(this);
        $.ajax({
            type: "POST",
            url: globalUrlSettings.EditFavoritesUrl,
            data: { ProductId: id },
            dataType: "JSON"
        }).success(function (data) {
            if (data.ifFavorited) {
                $this.switchClass("glyphicon-star-empty", "glyphicon-star", 1000, "easeInOutQuad");
            } else {
                $this.switchClass("glyphicon-star", "glyphicon-star-empty", 1000, "easeInOutQuad");
            }
        }).error(function (result) {
            alert("Error");
        });
    });
    
})