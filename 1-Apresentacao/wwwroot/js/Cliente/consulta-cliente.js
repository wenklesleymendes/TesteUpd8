function openEditModal(button) {
    console.log('Modal está sendo exibido'); // Depuração
    debugger;
    var clienteId = button.getAttribute('data-id');
    console.log('Cliente ID:', clienteId); // Depuração

    fetch(`/Cliente/GetCliente?id=${clienteId}`)
        .then(response => response.json())
        .then(data => {
            console.log('Dados recebidos:', data); // Depuração
            document.getElementById('editClienteId').value = data.id;
            document.getElementById('editNome').value = data.nome;
            document.getElementById('editCpf').value = data.cpf;
            document.getElementById('editDataNascimento').value = data.dataNascimento;
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

    fetch('/Cliente/Edit', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(cliente)
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                $('#editClienteModal').modal('hide');
                location.reload(); // Recarregar a página para ver as atualizações
            } else {
                alert('Erro ao salvar o cliente');
            }
        })
        .catch(error => {
            console.error('Erro ao salvar o cliente:', error); // Depuração
            alert('Erro ao salvar o cliente.');
        });
}