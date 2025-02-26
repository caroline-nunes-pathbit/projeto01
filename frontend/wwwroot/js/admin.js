const URL_BASE = "http://0.0.0.0:5000"

// Verifica se o usuário é um administrador
function isAdmin() {
    const userType = localStorage.getItem("userType");
    return userType === "Administrador";
}

// Função para carregar os produtos
async function loadProducts() {
    if (!isAdmin()) {
        window.location.href = "index.html"; // Redireciona caso não seja admin
        return;
    }

    const response = await fetch(`${URL_BASE}/api/products`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${localStorage.getItem("token")}`
        }
    });

    const products = await response.json();
    const tableBody = document.querySelector("#productList tbody");
    tableBody.innerHTML = ""; // Limpa a tabela antes de inserir

    products.forEach(product => {
        const row = document.createElement("tr");
        row.innerHTML = `
            <td>${product.name}</td>
            <td>${product.price}</td>
            <td>
                <button class="editBtn" data-id="${product.id}">Editar</button>
                <button class="deleteBtn" data-id="${product.id}">Excluir</button>
            </td>
        `;
        tableBody.appendChild(row);
    });

    document.querySelectorAll(".editBtn").forEach(button => {
        button.addEventListener("click", editProduct);
    });

    document.querySelectorAll(".deleteBtn").forEach(button => {
        button.addEventListener("click", deleteProduct);
    });
}

// Função para editar produto
function editProduct(event) {
    const productId = event.target.dataset.id;
    console.log("Editando produto:", productId);
    // Implemente o código para edição do produto
}

// Função para excluir produto
async function deleteProduct(event) {
    const productId = event.target.dataset.id;
    const response = await fetch(`${URL_BASE}/api/products/${productId}`, {
        method: "DELETE",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${localStorage.getItem("token")}`
        }
    });

    if (response.ok) {
        alert("Produto excluído com sucesso.");
        loadProducts(); // Recarregar lista de produtos
    } else {
        alert("Erro ao excluir o produto.");
    }
}

// Função para adicionar produto
document.getElementById("addProductButton").addEventListener("click", () => {
    // Implemente o código para adicionar produto
    alert("Função para adicionar produto!");
});

// Logout
document.getElementById("logoutButton").addEventListener("click", () => {
    localStorage.removeItem("token");
    localStorage.removeItem("userType");
    window.location.href = "index.html"; // Redireciona para a página inicial
});

// Carrega os produtos ao iniciar a página
loadProducts();

export { isAdmin, loadProducts, editProduct, deleteProduct }; // Exporta as funções
