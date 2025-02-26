const URL_BASE = "http://0.0.0.0:5000";

async function userRegister({ name, userName, userEmail, password, userType }) {
    console.log("📤 Tentando cadastrar usuário:", { name, userName, userEmail, password, userType });

    try {
        const response = await fetch(`${URL_BASE}/api/user/signup`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ name, userName, userEmail, password, userType })
        });

        console.log("🔄 Status da resposta:", response.status, response.statusText);

        const data = await response.json();
        console.log("📥 Resposta da API:", data);

        if (!response.ok) {
            console.error("🚨 Erro ao cadastrar:", data.message || "Erro desconhecido");
            throw new Error(`Erro ao cadastrar o usuário: ${data.message || "Erro desconhecido"}`);
        }

        console.log("✅ Cadastro realizado com sucesso!");
        return data;
    } catch (error) {
        console.error("🚨 Erro na requisição de cadastro:", error);
        throw error;
    }
}

// Função para login
async function userLogin({ userEmail, password }) {
    console.log("📤 Tentando login com:", { userEmail, password });

    try {
        const response = await fetch(`${URL_BASE}/api/user/login`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ userEmail, password })
        });

        console.log("🔄 Status da resposta:", response.status, response.statusText);

        let data;
        try {
            data = await response.json();
        } catch (jsonError) {
            console.error("⚠️ Resposta da API não é um JSON válido:", jsonError);
            throw new Error(`Erro inesperado. Status: ${response.status}`);
        }

        console.log("📥 Resposta da API:", data);

        if (!response.ok) {
            console.error("Erro no login:", data.message || "Credenciais inválidas");
            throw new Error(`Erro no login: ${data.message || "Credenciais inválidas"}`);
        }

        console.log("Login realizado com sucesso!");

        // Salva o tipo de usuário e o token no localStorage
        localStorage.setItem("token", data.token);
        localStorage.setItem("userType", data.userType); // Salva o tipo de usuário

        // Verifica e renderiza a interface
        renderInterface();

        return data;
    } catch (error) {
        console.error("Erro na requisição de login:", error);
        throw error;
    }
}

// Função para renderizar a interface com base no tipo de usuário
function renderInterface() {
    const userType = localStorage.getItem("userType");

    // Esconde ambas as interfaces
    document.getElementById('adminInterface').classList.add('hidden');
    document.getElementById('clientInterface').classList.add('hidden');

    // Verifica o tipo de usuário e exibe a interface correspondente
    if (userType === 'admin') {
        document.getElementById('adminInterface').classList.remove('hidden');
    } else if (userType === 'client') {
        document.getElementById('clientInterface').classList.remove('hidden');
    } else {
        console.error("Tipo de usuário desconhecido.");
    }
}

// Verificar Autenticação
function isAuthenticated() {
    const token = localStorage.getItem("token");
    console.log(`Verificando autenticação: ${token ? "Autenticado" : "Não autenticado"}`);
    return token !== null;
}

window.onload = function() {
    if (isAuthenticated()) {
        renderInterface(); // Se já estiver autenticado, renderiza a interface automaticamente
    } else {
        alert("Você não está autenticado. Por favor, faça login.");
    }
};

// Obter Tipo de Usuário
function getUserType() {
    const userType = localStorage.getItem("userType");
    console.log("Tipo de usuário:", userType);
    return userType;
}

// Logout
function logout() {
    console.log("Realizando logout");
    localStorage.removeItem("token");
    localStorage.removeItem("userType");
    console.log("Token e tipo de usuário removidos.");
    window.location.href = "index.html"; // Redirecionar para a página inicial
}

// Função para incluir o token no cabeçalho das requisições autenticadas
function getAuthHeaders() {
    const token = localStorage.getItem("token");
    if (!token) {
        console.error("Usuário não autenticado. Token não encontrado.");
        throw new Error("Usuário não autenticado.");
    }

    console.log("Cabeçalho de autenticação gerado.");
    return {
        "Content-Type": "application/json",
        "Authorization": `Bearer ${token}`
    };
}

export { userRegister, userLogin, renderInterface, isAuthenticated, getUserType, logout, getAuthHeaders };
