$(document).ready(function () {
    $('#editClienteModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget);
        var clienteId = button.data('id');
        console.log('Cliente ID:', clienteId); // Depuração

        $.ajax({
            url: '/Cliente/GetCliente', // Certifique-se de que a URL está correta
            type: 'GET',
            data: { id: clienteId },
            success: function (data) {
                console.log('Dados recebidos:', data); // Depuração
                $('#editClienteId').val(data.id);
                $('#editNome').val(data.nome);
                $('#editCpf').val(data.cpf);
                $('#editDataNascimento').val(data.dataNascimento.split('T')[0]);
                $('#editEstado').val(data.estado);
                $('#editCidade').val(data.cidade);
                $('#editSexo').val(data.sexo);
            },
            error: function (xhr, status, error) {
                console.error('Erro ao carregar os dados do cliente:', error); // Depuração
                alert('Erro ao carregar os dados do cliente.');
            }
        });
    });

    $('#saveEditCliente').on('click', function () {
        var cliente = {
            Id: $('#editClienteId').val(),
            Nome: $('#editNome').val(),
            Cpf: $('#editCpf').val(),
            DataNascimento: $('#editDataNascimento').val(),
            Estado: $('#editEstado').val(),
            Cidade: $('#editCidade').val(),
            Sexo: $('#editSexo').val()
        };

        console.log('Dados para salvar:', cliente); // Depuração

        $.ajax({
            url: '/Cliente/Edit', // Certifique-se de que a URL está correta
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(cliente),
            success: function (data) {
                if (data.success) {
                    $('#editClienteModal').modal('hide');
                    location.reload(); // Recarregar a página para ver as atualizações
                } else {
                    alert('Erro ao salvar o cliente');
                }
            },
            error: function (xhr, status, error) {
                console.error('Erro ao salvar o cliente:', error); // Depuração
                alert('Erro ao salvar o cliente.');
            }
        });
    });
});
