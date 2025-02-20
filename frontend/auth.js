const API_BASE_URL = 'http://localhost:5000/api';

// User Registration
async function registerUser(userData) {
    try {
        const response = await fetch(`${API_BASE_URL}/users/signup`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(userData)
        });
        return await response.json();
    } catch (error) {
        console.error('Erro no registro:', error);
        throw error;
    }
}

// User Login
async function loginUser(credentials) {
    try {
        const response = await fetch(`${API_BASE_URL}/users/login`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(credentials)
        });
        const data = await response.json();
        if (data.token) {
            localStorage.setItem('token', data.token);
            localStorage.setItem('userType', data.userType);
        }
        return data;
    } catch (error) {
        console.error('Erro no login:', error);
        throw error;
    }
}

// Check Authentication
function isAuthenticated() {
    return localStorage.getItem('token') !== null;
}

// Get User Type
function getUserType() {
    return localStorage.getItem('userType');
}

// Logout
function logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('userType');
    window.location.href = '/';
}

export { registerUser, loginUser, isAuthenticated, getUserType, logout };
