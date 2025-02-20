const API_BASE_URL = 'http://localhost:5000/api';

// Get All Products
async function getProducts() {
    try {
        const response = await fetch(`${API_BASE_URL}/products`);
        return await response.json();
    } catch (error) {
        console.error('Erro ao buscar produtos:', error);
        throw error;
    }
}

// Create Order
async function createOrder(orderData) {
    try {
        const response = await fetch(`${API_BASE_URL}/orders`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.getItem('token')}`
            },
            body: JSON.stringify(orderData)
        });
        return await response.json();
    } catch (error) {
        console.error('Erro ao criar pedido:', error);
        throw error;
    }
}

// Get User Orders
async function getUserOrders(userId) {
    try {
        const response = await fetch(`${API_BASE_URL}/orders?userId=${userId}`, {
            headers: {
                'Authorization': `Bearer ${localStorage.getItem('token')}`
            }
        });
        return await response.json();
    } catch (error) {
        console.error('Erro ao buscar pedidos:', error);
        throw error;
    }
}

export { getProducts, createOrder, getUserOrders };
