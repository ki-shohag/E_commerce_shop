$(document).ready(function () {
    mydata = JSON.stringify({ "Category": "Laptop", "Discount": 10.00 });

    $.ajax({
        type: "POST",
        url: "/Products/GetAllProductsKeyValue",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: mydata,
        success: function (result) {
            var tags = JSON.parse(result);
            //nsole.log(tags);
            $('#searchBox').autocomplete({
                source: tags,
                select: showResult
            }) 
            function showResult(event, ui) {
                //ert(ui.item.label);
                var name = ui.item.label;
                mydata = JSON.stringify({ "name": name });
                $.ajax({
                    type: "POST",
                    url: "/Products/GetProductIdByProductName",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: mydata,
                    success: function (result) {
                        //ert(result);
                        window.location.replace("/Products/ShowProduct/" + result);
                    }
                });
            }
        }
    });
});