$("document").ready(
    function () {
        $(".link").click(
            function () {
                let uri = "http://localhost:5092/Catalog/GetTreatments/?"
                if (this.hasAttribute("filterInput")) {
                    let ext = "name=" + this.getAttribute("filterInput");
                    uri = uri + ext;
                }
                $.ajax({
                    url: uri,
                    method: "GET",
                    dataType: "html",
                    beforeSend: function () {
                        let loader = "<div class=loader>" +
                            "<img src='content/images/loader.gif' />" +
                            "</div>";
                        $("#main").html(loader);

                    },
                    error: function () {
                        // your code here;
                    },
                    success: function (data) {
                        setTimeout(function () {
                            $("#main").html(data);
                        }, 2000);

                    },
                    complete: function () {
                        // your code here;
                    }
                });
            }
        );
    });