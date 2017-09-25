window.onload = function () {
    if (window.File && window.FileList && window.FileReader) {
        $('#clear').click(function () {
            if ($('.col-xs-6.col-md-3.image-preview').length > 0) {
                $(".col-xs-6.col-md-3.image-preview").remove();
                $('#files').val("");
                $(this).hide();
            } else {
                alert("No images!");
            }
                
        });

        $('#files').on("change", function (event) {
            if ($('.col-xs-6.col-md-3.image-preview').length > 0) {
                $(".col-xs-6.col-md-3.image-preview").remove();
                $('#files').val("");
            }
            var files = event.target.files; //FileList object
            var placeholder = document.getElementById("permanentimage");
            var len = files.length;
            if ($('.imageholder').length + len <= 10) {
                for (var i = 0; i < len; i++) {
                    var file = files[i];
                    if (file.type.match('image.*')) {
                        if (this.files[0].size < 2097152) {
                            // continue;
                            var picReader = new FileReader();
                            picReader.onload = (function (theFile) {
                                return function (e) {
                                    var new1 = $('<div />', {
                                        "class": 'col-xs-6 col-md-3 image-preview'
                                    });
                                    var new2 = $('<div />', {
                                        "class": 'thumbnail'
                                    });
                                    var img = $('<img />', {
                                        src: e.target.result,
                                        "class": 'imageholder',
                                        alt: 'image'
                                    });
                                    $(new1.append(new2.append(img))).insertBefore(placeholder);
                                };
                            })(file);
                            //Read the image
                            $('#clear').show();
                            //document.getElementById(placeholder).show();
                            picReader.readAsDataURL(file);
                        } else {
                            alert("Image Size is too big. Minimum size is 2MB.");
                            $(this).val("");
                        }
                    } else {
                        alert("You can only upload image file.");
                        $(this).val("");
                    }
                }
            } else {
                alert("You can not upload  more than 10 images!");
                $(this).val("");
            }
        });
    }
    else {
        console.log("Your browser does not support File API");
    }
};

BootstrapDialog.show({
    message: 'Advertisement Created Successfully! Please upload related picturese, SKIP if you dont want to.',
    buttons: [{
        label: 'Confirm',
        cssClass: 'btn-primary',
        action: function (dialogItself) {
            dialogItself.close();
        }
    }]
});