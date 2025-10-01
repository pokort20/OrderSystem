function applyFilter() {
    const status = document.getElementById('statusCode').value;
    const customer = document.getElementById('customerName').value;

    fetch(`/Orders/FilterTotals?statusCode=${encodeURIComponent(status)}&customerName=${encodeURIComponent(customer)}`)
        .then(response => response.text())
        .then(html => {
            document.getElementById('ordersTotals').innerHTML = html;
        });

    fetch(`/Orders/FilterRows?statusCode=${encodeURIComponent(status)}&customerName=${encodeURIComponent(customer)}`)
        .then(response => response.text())
        .then(html => {
            document.getElementById('ordersTable').innerHTML = html;
        });
}

document.addEventListener('DOMContentLoaded', function () {
    document.getElementById('statusCode').addEventListener('change', applyFilter);
    document.getElementById('customerName').addEventListener('input', function () {
        clearTimeout(this.delay);
        this.delay = setTimeout(applyFilter, 500); //Preventing querying while typing
    });
});
