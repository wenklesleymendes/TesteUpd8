function openEditModal(button) {

    var clienteId = button.getAttribute('data-id');

    fetch(`/Cliente/GetCliente?id=${clienteId}`)
        .then(response => response.json())
        .then(data => {
            console.log('Dados recebidos:', data); // Depuração
            document.getElementById('editClienteId').value = data.id;
            document.getElementById('editNome').value = data.nome;
            document.getElementById('editCpf').value = data.cpf;
            document.getElementById('editDataNascimento').value = data.dataNascimento;
            document.getElementById('editEndereco').value = data.endereco;
            document.getElementById('editEstado').value = data.estado;
            document.getElementById('editCidade').value = data.cidade;
            document.getElementById('editSexo').value = data.sexo;
            $('#editClienteModal').modal('show');
        })
        .catch(error => {
            console.error('Erro ao carregar os dados do cliente:', error); // Depuração
            alert('Erro ao carregar os dados do cliente.');
        });
}

function saveEditCliente() {
    debugger;
    var cliente = {
        Id: document.getElementById('editClienteId').value,
        Nome: document.getElementById('editNome').value,
        Cpf: document.getElementById('editCpf').value,
        DataNascimento: document.getElementById('editDataNascimento').value,
        Estado: document.getElementById('editEstado').value,
        Cidade: document.getElementById('editCidade').value,
        Sexo: document.getElementById('editSexo').value
    };

    console.log('Dados para salvar:', cliente); // Depuração

    fetch('/Cliente/Editar', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': document.getElementsByName('__RequestVerificationToken')[0].value // Token de validação anti-CSRF
        },
        body: JSON.stringify(cliente)
    })
        .then(response => {
            console.log('Response status:', response.status);
            console.log('Response status text:', response.statusText);
            return response.text().then(text => {
                console.log('Response text:', text);
                try {
                    return text ? JSON.parse(text) : {}; // Garante que o texto não esteja vazio antes de analisar
                } catch (error) {
                    throw new Error('Erro ao analisar a resposta JSON: ' + error.message + ' - Resposta: ' + text);
                }
            });
        })
        .then(data => {
            console.log('Dados recebidos após salvar:', data); // Depuração
            if (data.success) {
                $('#editClienteModal').modal('hide');
                location.reload(); // Recarregar a página para ver as atualizações
            } else {
                alert('Erro ao salvar o cliente: ' + (data.message || 'Erro desconhecido'));
            }
        })
        .catch(error => {
            console.error('Erro ao salvar o cliente:', error); // Depuração
            alert('Erro ao salvar o cliente: ' + error.message);
        });
}

