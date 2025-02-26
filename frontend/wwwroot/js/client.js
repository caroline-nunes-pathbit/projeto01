const URL_BASE = "http://0.0.0.0:5000"

// Import the isAuthenticated function from auth.js
import { isAuthenticated } from './auth.js';

// Verifica se o usuário é um cliente
function isClient() {
    const userType = localStorage.getItem("userType");
    return userType === "Cliente";
}

// Função para carregar os pedidos
async function loadOrders() {
    if (!isClient()) {
        window.location.href = "index.html"; // Redireciona caso não seja cliente
        return;
    }

    try {
        const orders = await getOrders();
        renderOrders(orders);
    } catch (error) {
        alert("Erro ao carregar pedidos: " + error.message);
    }
}

// Função para carregar os produtos
async function loadProducts() {
    if (!isClient()) {
        window.location.href = "index.html"; // Redireciona caso não seja cliente
        return;
    }

    try {
        const products = await getProducts();
        renderProductOptions(products);
    } catch (error) {
        alert("Erro ao carregar produtos: " + error.message);
    }
}

// Criar pedido
const form = document.getElementById('createOrderForm');
if (form) {
    form.addEventListener('submit', async (e) => {
        e.preventDefault();

        const orderData = {
            clientName: document.getElementById('clientName').value,
            productId: document.getElementById('product').value,  // Supondo que seja um ID de produto
            quantity: document.getElementById('quantity').value,
            address: document.getElementById('address').value,
            cep: document.getElementById('cep').value,
        };

        try {
            await createOrder(orderData);
            alert("Pedido criado com sucesso!");
            loadOrders();  // Recarrega a lista de pedidos
        } catch (error) {
            alert("Erro ao criar pedido: " + error.message);
        }
    });
}

// Função para criar um pedido
async function createOrder(orderData) {
    try {
        const response = await fetch(`${URL_BASE}/api/orders`, {
            method: 'POST',
            headers: getAuthHeaders(),
            body: JSON.stringify(orderData),
        });

        if (!response.ok) {
            throw new Error('Falha ao criar pedido');
        }
        return await response.json();
    } catch (error) {
        throw error;
    }
}

// Função para listar todos os pedidos
async function getOrders() {
    try {
        const response = await fetch(`${URL_BASE}/api/orders`, {
            method: 'GET',
            headers: getAuthHeaders(),
        });

        if (!response.ok) {
            throw new Error('Falha ao carregar pedidos');
        }
        return await response.json();
    } catch (error) {
        throw error;
    }
}

// Função para excluir um produto de um pedido
async function deleteProductFromOrder(orderId, productId) {
    try {
        const response = await fetch(`${URL_BASE}/api/orders`, {
            method: 'DELETE',
            headers: getAuthHeaders(),
        });

        if (!response.ok) {
            throw new Error('Falha ao excluir produto do pedido');
        }

        alert('Produto excluído do pedido com sucesso!');
        loadOrders();  // Recarrega a lista de pedidos
    } catch (error) {
        alert('Erro ao excluir produto do pedido: ' + error.message);
    }
}

// Função para carregar os produtos (opções de produto no formulário)
async function getProducts() {
    try {
        const response = await fetch(`${URL_BASE}/api/products`, {
            method: 'GET',
            headers: getAuthHeaders(),
        });

        if (!response.ok) {
            throw new Error('Falha ao carregar produtos');
        }
        return await response.json();
    } catch (error) {
        throw error;
    }
}

// Função para renderizar os produtos no select
function renderProductOptions(products) {
    const productSelect = document.getElementById('product');
    productSelect.innerHTML = ''; // Limpa as opções anteriores

    products.forEach(product => {
        const option = document.createElement('option');
        option.value = product.id;
        option.textContent = `${product.name}, - R$ ${product.price}`;
        productSelect.appendChild(option);
    });
}

// Renderizar pedidos
function renderOrders(orders) {
    const ordersList = document.getElementById('ordersList');
    ordersList.innerHTML = '';  // Limpa a lista existente

    if (orders.length === 0) {
        ordersList.innerHTML = '<p>Você ainda não tem pedidos.</p>';
        return;
    }

    orders.forEach((order) => {
        const orderDiv = document.createElement('div');
        orderDiv.classList.add('order');

        orderDiv.innerHTML = 
            `<h3>Pedido #${order.id}</h3>
            <p>Nome: ${order.clientName}</p>
            <p>Produto: ${order.productName}</p>
            <p>Quantidade: ${order.quantity}</p>
            <p>Endereço: ${order.address}</p>
            <p>CEP: ${order.cep}</p>
            <p>Status: ${order.status ? 'Enviado' : 'Não Enviado'}</p>
            <p>Data: ${new Date(order.createdAt).toLocaleString()}</p>
            <button onclick="deleteProductFromOrder(${order.id}, ${order.productId})">Excluir Produto do Pedido</button>`;

        ordersList.appendChild(orderDiv);
    });
}

// Logout
document.getElementById("logoutBtn").addEventListener("click", () => {
    localStorage.removeItem("token");
    localStorage.removeItem("userType");
    window.location.href = "index.html"; // Redireciona para a página inicial
});

// Inicializar a página com os pedidos e produtos
document.addEventListener('DOMContentLoaded', () => {
    if (isAuthenticated()) {
        loadOrders();  // Carrega os pedidos assim que a página for carregada
        loadProducts();  // Carrega os produtos para o formulário
    } else {
        alert("Você não está autenticado. Por favor, faça login.");
    }

});

export { isClient, loadOrders, loadProducts, createOrder, getOrders, deleteProductFromOrder,
    getProducts, renderProductOptions, renderOrders
 };
