import { userRegister, userLogin, isAuthenticated, getUserType, logout } from './auth.js';
import { getProducts, } from './client.js';

// Initialize the app
document.addEventListener('DOMContentLoaded', () => {
    checkAuthState();
    setupEventListeners();
});

function checkAuthState() {
    if (isAuthenticated()) {
        const userType = getUserType();
        if (userType === 'Administrador') {
            showAdminInterface();
        } else if (userType === 'Cliente') {
            showClientInterface();
        } else {
            alert("Tipo de usuário desconhecido. Por favor, faça login novamente.");
            showLoginForm();
        }
    } else {
        showLoginForm();
    }

}

function setupEventListeners() {
    // Handle browser back/forward navigation
    window.addEventListener('popstate', () => {
        if (window.location.pathname === '/') {
            showLoginForm();
        } else if (window.location.pathname === '/signup') {
            showRegisterForm();
        }
    });

    document.getElementById('showRegister')?.addEventListener('click', (e) => {
        e.preventDefault();
        showRegisterForm();
    });

    document.getElementById('showLogin')?.addEventListener('click', (e) => {
        e.preventDefault();
        showLoginForm();
    });

    // Login form
document.getElementById('loginForm').addEventListener('submit', async (e) => {
    e.preventDefault(); // Prevent default form submission

        e.preventDefault();

        // Captura os valores corretamente
        const userEmail = document.getElementById('email').value.trim();
        const password = document.getElementById('passwordLogin').value.trim();

        console.log("📋 Dados capturados do formulário:", { userEmail, password });

        try {
            await userLogin({ userEmail, password }); // Envia com os nomes corretos
            checkAuthState();
        } catch (error) {
            showError('Erro no login: ' + error.message);
        }
    });

    // Register form
    document.getElementById('registerForm').addEventListener('submit', async (e) => {
        console.log('Register form submitted'); // Debugging log

        e.preventDefault();
        const userData = {
            name: document.getElementById('name').value,
            userName: document.getElementById('userName').value,
            userEmail: document.getElementById('userEmail').value,
            password: document.getElementById('password').value,
            userType: document.getElementById('userType').value
        };
        
        console.log(userData);
        try {
            await userRegister(userData);
            showSuccess('Usuário registrado com sucesso!');
        } catch (error) {
            showError('Erro no registro: ' + error.message);
        }
    });

    // Logout button
    document.getElementById('logoutBtn')?.addEventListener('click', () => {
        logout();
        checkAuthState();
    });
}

function showAdminInterface() {
    document.getElementById('loginContainer').style.display = 'none';
    document.getElementById('clientInterface').style.display = 'none';
    document.getElementById('registerContainer').style.display = 'none';
    document.getElementById('adminInterface').style.display = 'block';
    loadAdminProducts();  // Carrega os produtos para o admin
}

function showClientInterface() {
    document.getElementById('loginContainer').style.display = 'none';
    document.getElementById('adminInterface').style.display = 'none';
    document.getElementById('registerContainer').style.display = 'none';
    document.getElementById('clientInterface').style.display = 'block';
    loadClientProducts();  // Carrega os produtos para o cliente
}

function showLoginForm() {
    document.getElementById('adminInterface').style.display = 'none';
    document.getElementById('clientInterface').style.display = 'none';
    document.getElementById('registerContainer').style.display = 'none';
    document.getElementById('loginContainer').style.display = 'block';
    window.history.pushState({}, '', '/');
}

function showRegisterForm() {
    document.getElementById('adminInterface').style.display = 'none';
    document.getElementById('clientInterface').style.display = 'none';
    document.getElementById('loginContainer').style.display = 'none';
    document.getElementById('registerContainer').style.display = 'block';
    window.history.pushState({}, '', '/signup');
}

async function loadAdminProducts() {
    try {
        const products = await getProducts();
        renderAdminProducts(products);
    } catch (error) {
        showError('Erro ao carregar produtos: ' + error.message);
    }
}

async function loadClientProducts() {
    try {
        const products = await getClientProducts();
        renderClientProducts(products);
    } catch (error) {
        showError('Erro ao carregar produtos: ' + error.message);
    }
}

function renderAdminProducts(products) {
    const productList = document.getElementById('productList');
    productList.innerHTML = ''; // Limpa a lista antes de preencher com novos produtos

    products.forEach(product => {
        const productRow = document.createElement('tr');
        
        productRow.innerHTML = `
            <td>${product.name}</td>
            <td>${product.price}</td>
            <td>${product.stock}</td>
            <td>
                <button class="editBtn" onclick="editProduct(${product.id})">Editar</button>
                <button class="deleteBtn" onclick="deleteProduct(${product.id})">Excluir</button>
            </td>
        `;
        productList.appendChild(productRow);
    });
}

function renderClientProducts(products) {
    const productList = document.getElementById('productList');
    productList.innerHTML = ''; // Limpa a lista antes de preencher com novos produtos

    products.forEach(product => {
        const productRow = document.createElement('tr');
        
        productRow.innerHTML = `
            <td>${product.name}</td>
            <td>${product.price}</td>
            <td>${product.stock}</td>
        `;
        productList.appendChild(productRow);
    });
}

function showError(message) {
    const errorDiv = document.getElementById('errorMessage');
    errorDiv.textContent = message;
    errorDiv.style.display = 'block';
}

function showSuccess(message) {
    const successDiv = document.getElementById('successMessage');
    successDiv.textContent = message;
    successDiv.style.display = 'block';
}
