import { registerUser, loginUser, isAuthenticated, getUserType, logout } from './auth.js';
import { addProduct, getProducts, updateProduct, deleteProduct } from './admin.js';
import { getProducts as getClientProducts, createOrder, getUserOrders } from './client.js';

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
        } else {
            showClientInterface();
        }
    } else {
        showLoginForm();
    }
}

function setupEventListeners() {
    // Login form
    document.getElementById('loginForm')?.addEventListener('submit', async (e) => {
        e.preventDefault();
        const email = document.getElementById('email').value;
        const password = document.getElementById('password').value;
        try {
            await loginUser({ email, password });
            checkAuthState();
        } catch (error) {
            showError('Erro no login: ' + error.message);
        }
    });

    // Register form
    document.getElementById('registerForm')?.addEventListener('submit', async (e) => {
        e.preventDefault();
        const userData = {
            email: document.getElementById('regEmail').value,
            password: document.getElementById('regPassword').value,
            fullName: document.getElementById('fullName').value,
            userType: document.getElementById('userType').value
        };
        try {
            await registerUser(userData);
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
    // Hide other interfaces
    document.getElementById('loginContainer').style.display = 'none';
    document.getElementById('clientInterface').style.display = 'none';
    
    // Show admin interface
    document.getElementById('adminInterface').style.display = 'block';
    loadProducts();
}

function showClientInterface() {
    // Hide other interfaces
    document.getElementById('loginContainer').style.display = 'none';
    document.getElementById('adminInterface').style.display = 'none';
    
    // Show client interface
    document.getElementById('clientInterface').style.display = 'block';
    loadClientProducts();
}

function showLoginForm() {
    document.getElementById('adminInterface').style.display = 'none';
    document.getElementById('clientInterface').style.display = 'none';
    document.getElementById('loginContainer').style.display = 'block';
}

async function loadProducts() {
    try {
        const products = await getProducts();
        renderProducts(products);
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
