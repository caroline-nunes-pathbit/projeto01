const API_BASE_URL = 'http://localhost:5000/api';

// Add Product
async function addProduct(productData) {
    try {
        const response = await fetch(`${API_BASE_URL}/products`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.getItem('token')}`
            },
            body: JSON.stringify(productData)
        });
        return await response.json();
    } catch (error) {
        console.error('Erro ao adicionar produto:', error);
        throw error;
    }
}

// Get All Products
async function getProducts() {
    try {
        const response = await fetch(`${API_BASE_URL}/products`, {
            headers: {
                'Authorization': `Bearer ${localStorage.getItem('token')}`
            }
        });
        return await response.json();
    } catch (error) {
        console.error('Erro ao buscar produtos:', error);
        throw error;
    }
}

// Update Product
async function updateProduct(productId, productData) {
    try {
        const response = await fetch(`${API_BASE_URL}/products/${productId}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.getItem('token')}`
            },
            body: JSON.stringify(productData)
        });
        return await response.json();
    } catch (error) {
        console.error('Erro ao atualizar produto:', error);
        throw error;
    }
}

// Delete Product
async function deleteProduct(productId) {
    try {
        const response = await fetch(`${API_BASE_URL}/products/${productId}`, {
            method: 'DELETE',
            headers: {
                'Authorization': `Bearer ${localStorage.getItem('token')}`
            }
        });
        return await response.json();
    } catch (error) {
        console.error('Erro ao deletar produto:', error);
        throw error;
    }
}

export { addProduct, getProducts, updateProduct, deleteProduct };
