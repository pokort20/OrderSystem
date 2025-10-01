document.getElementById("addOrderForm").addEventListener("submit", function (e) {
    e.preventDefault();

    const data = {
        CustomerId: parseInt(document.getElementById("customerId").value),
        StatusId: parseInt(document.getElementById("statusId").value),
        Amount: parseFloat(document.getElementById("amount").value),
        Date: new Date().toISOString()
    };

    fetch("/api/OrdersApi", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(data)
    })
        .then(res => res.json())
        .then(response => {
            alert("Order added successfully!");
            if (typeof applyFilter === "function") applyFilter();
            document.getElementById("amount").value = "";
        })
        .catch(err => alert("Error: " + err));
});
