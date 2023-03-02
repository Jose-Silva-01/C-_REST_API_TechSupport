////window.onload = function () {
////    document.querySelector("#deleteCustomersLink").addEventListener("click", handleDeleteCustomer);
////}
$(".deleteStatesForm").submit(function (e) {
    e.preventDefault();

    var form = $(this);
    var actionUrl = form.attr('action');

    $.ajax({
        type: "POST",
        url: actionUrl,
        data: form.serialize(),
        success: function (data) {
            alert(data.message);
            location.reload();
            //window.location.href = "https://localhost:44305/Customers/CustomersDisplay";
        }
    });
});

$(".addStatesForm").submit(function (e) {
    e.preventDefault();

    var form = $(this);
    var actionUrl = form.attr('action');

    $.ajax({
        type: "POST",
        url: actionUrl,
        data: form.serialize(),
        success: function (data) {
            alert(data.message);
            location.reload();
            //window.location.href = "https://localhost:44305/Customers/CustomersDisplay";
        }
    });
});

$(".deleteTechniciansForm").submit(function (e) {
    e.preventDefault();

    var form = $(this);
    var actionUrl = form.attr('action');

    $.ajax({
        type: "POST",
        url: actionUrl,
        data: form.serialize(),
        success: function (data) {
            alert(data.message);
            location.reload();
            //window.location.href = "https://localhost:44305/Customers/CustomersDisplay";
        }
    });
});

$(".addTechnicianForm").submit(function (e) {
    e.preventDefault();

    var form = $(this);
    var actionUrl = form.attr('action');

    $.ajax({
        type: "POST",
        url: actionUrl,
        data: form.serialize(),
        success: function (data) {
            alert(data.message);
            location.reload();
            //window.location.href = "https://localhost:44305/Customers/CustomersDisplay";
        }
    });
});

$(".deleteIncidentsForm").submit(function (e) {
    e.preventDefault();

    var form = $(this);
    var actionUrl = form.attr('action');

    $.ajax({
        type: "POST",
        url: actionUrl,
        data: form.serialize(),
        success: function (data) {
            alert(data.message);
            location.reload();
            //window.location.href = "https://localhost:44305/Customers/CustomersDisplay";
        }
    });
});

$(".addIncidentForm").submit(function (e) {
    e.preventDefault();

    var form = $(this);
    var actionUrl = form.attr('action');

    $.ajax({
        type: "POST",
        url: actionUrl,
        data: form.serialize(),
        success: function (data) {
            alert(data.message);
            location.reload();
            //window.location.href = "https://localhost:44305/Customers/CustomersDisplay";
        }
    });
});

$(".deleteRegistrationsForm").submit(function (e) {
    e.preventDefault();

    var form = $(this);
    var actionUrl = form.attr('action');

    $.ajax({
        type: "POST",
        url: actionUrl,
        data: form.serialize(),
        success: function (data) {
            alert(data.message);
            location.reload();
            //window.location.href = "https://localhost:44305/Customers/CustomersDisplay";
        }
    });
});

$(".addRegistrationForm").submit(function (e) {
    e.preventDefault();

    var form = $(this);
    var actionUrl = form.attr('action');

    $.ajax({
        type: "POST",
        url: actionUrl,
        data: form.serialize(),
        success: function (data) {
            alert(data.message);
            location.reload();
            //window.location.href = "https://localhost:44305/Customers/CustomersDisplay";
        }
    });
});

$(".deleteProductsForm").submit(function (e) {
    e.preventDefault();

    var form = $(this);
    var actionUrl = form.attr('action');

    $.ajax({
        type: "POST",
        url: actionUrl,
        data: form.serialize(),
        success: function (data) {
            alert(data.message);
            location.reload();
            //window.location.href = "https://localhost:44305/Customers/CustomersDisplay";
        }
    });
});

$(".addProductForm").submit(function (e) {
    e.preventDefault();

    var form = $(this);
    var actionUrl = form.attr('action');

    $.ajax({
        type: "POST",
        url: actionUrl,
        data: form.serialize(),
        success: function (data) {
            alert(data.message);
            location.reload();
            //window.location.href = "https://localhost:44305/Customers/CustomersDisplay";
        }
    });
});

$(".addCustomerForm").submit(function (e) {
    e.preventDefault();

    var form = $(this);
    var actionUrl = form.attr('action');

    $.ajax({
        type:"POST",
        url: actionUrl,
        data: form.serialize(),
        success: function (data) {
            alert(data.message);
            location.reload();
            //window.location.href = "https://localhost:44305/Customers/CustomersDisplay";
        }
    });
});

$(".deleteCustomerForm").submit(function (e) {
    e.preventDefault();

    var form = $(this);
    var actionUrl = form.attr('action');

    $.ajax({
        type: "POST",
        url: actionUrl,
        data: form.serialize(),
        success: function (data) {
            alert(data.message);
            location.reload();
            //window.location.href = "https://localhost:44305/Customers/CustomersDisplay";
        }
    });
});



function createPaging(page, top, totalItems, baseURL, searchTerm, entity) {

    var numPages = totalItems / top;


    if (numPages % 2 == 1) {
        numPages += 1;
    }

    var _HTMLPaging = [];

    for (var i = 0; i < numPages; i++) {
        var activeClass = '';
        if (page == (i + 1)) {
            activeClass = 'active';
        }
        _HTMLPaging.push('<li class="page-item ' + activeClass + ' "><a class="page-link" href="' + baseURL + '?top=' + top + '&page=' + (i + 1).toString() + '&search'+entity+'=' + searchTerm + '">' + (i + 1).toString() + '</a></li>');
    }

    $(".pagination").html(_HTMLPaging.join(''));
};

function handleDeleteCustomer() {
    alert("This is just a test");
}