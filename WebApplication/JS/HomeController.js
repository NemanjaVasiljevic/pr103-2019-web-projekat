$(document).ready(function () {
    $('#content').show(loadGyms);
    $("#btnSortNameAsc").click(function SortGymsByNameDesc() {
        $.get("/api/Teretana", function (data, status) {
            data.sort((a, b) => a.Naziv.localeCompare(b.Naziv));
            let table = '<table>';
            table += '<tr><th>Naziv</th><th>Adresa</th><th>Godina otvaranja</th><th></th></tr>';
            for (element in data) {
                let teretana = '<td>' + data[element].Naziv + '</td>';
                teretana += '<td>' + data[element].Adresa.Ulica + ' ' + data[element].Adresa.Broj + ', ' + data[element].Adresa.Grad + '</td>';
                teretana += '<td>' + data[element].GodinaOtvaranja + '</td>';
                teretana += '<td><input type="submit" class="btn" value="Detalji"/></td>';
                table += '<tr> ' + teretana + '</tr>';
            }
            table += '</table>';
            $('#content').html(table);
        });
    });
    $("#btnSortNameDesc").click(function SortGymsByNameDesc() {
        $.get("/api/Teretana", function (data, status) {
            data.sort((a, b) => b.Naziv.localeCompare(a.Naziv));
            let table = '<table>';
            table += '<tr><th>Naziv</th><th>Adresa</th><th>Godina otvaranja</th><th></th></tr>';
            for (element in data) {
                let teretana = '<td>' + data[element].Naziv + '</td>';
                teretana += '<td>' + data[element].Adresa.Ulica + ' ' + data[element].Adresa.Broj + ', ' + data[element].Adresa.Grad + '</td>';
                teretana += '<td>' + data[element].GodinaOtvaranja + '</td>';
                teretana += '<td><input type="submit" class="btn" value="Detalji"/></td>';
                table += '<tr> ' + teretana + '</tr>';
            }
            table += '</table>';
            $('#content').html(table);
        });
    });

    $("#btnSortAddressAsc").click(function SortGymsByNameAsc() {
        $.get("/api/Teretana", function (data, status) {
            data.sort((a, b) => a.Adresa.Ulica.localeCompare(b.Adresa.Ulica));
            let table = '<table>';
            table += '<tr><th>Naziv</th><th>Adresa</th><th>Godina otvaranja</th><th></th></tr>';
            for (element in data) {
                let teretana = '<td>' + data[element].Naziv + '</td>';
                teretana += '<td>' + data[element].Adresa.Ulica + ' ' + data[element].Adresa.Broj + ', ' + data[element].Adresa.Grad + '</td>';
                teretana += '<td>' + data[element].GodinaOtvaranja + '</td>';
                teretana += '<td><input type="submit" class="btn" value="Detalji"/></td>';
                table += '<tr> ' + teretana + '</tr>';
            }
            table += '</table>';
            $('#content').html(table);
        });
    });
    $("#btnSortAddressDesc").click(function SortGymsByNameAsc() {
        $.get("/api/Teretana", function (data, status) {
            data.sort((a, b) => b.Adresa.Ulica.localeCompare(a.Adresa.Ulica));
            let table = '<table>';
            table += '<tr><th>Naziv</th><th>Adresa</th><th>Godina otvaranja</th><th></th></tr>';
            for (element in data) {
                let teretana = '<td>' + data[element].Naziv + '</td>';
                teretana += '<td>' + data[element].Adresa.Ulica + ' ' + data[element].Adresa.Broj + ', ' + data[element].Adresa.Grad + '</td>';
                teretana += '<td>' + data[element].GodinaOtvaranja + '</td>';
                teretana += '<td><input type="submit" class="btn" value="Detalji"/></td>';
                table += '<tr> ' + teretana + '</tr>';
            }
            table += '</table>';
            $('#content').html(table);
        });
    });

    $("#btnSortGodiniAsc").click(function SortGymsByNameAsc() {
        $.get("/api/Teretana", function (data, status) {
            data.sort((a, b) => {
                if (a.GodinaOtvaranja > b.GodinaOtvaranja) return 1;
                if (a.GodinaOtvaranja < b.GodinaOtvaranja) return -1;
                return 0;
            });
            let table = '<table>';
            table += '<tr><th>Naziv</th><th>Adresa</th><th>Godina otvaranja</th><th></th></tr>';
            for (element in data) {
                let teretana = '<td>' + data[element].Naziv + '</td>';
                teretana += '<td>' + data[element].Adresa.Ulica + ' ' + data[element].Adresa.Broj + ', ' + data[element].Adresa.Grad + '</td>';
                teretana += '<td>' + data[element].GodinaOtvaranja + '</td>';
                teretana += '<td><input type="submit" class="btn" value="Detalji"/></td>';
                table += '<tr> ' + teretana + '</tr>';
            }
            table += '</table>';
            $('#content').html(table);
        });
    });

    $("#btnSortGodiniDesc").click(function SortGymsByNameAsc() {
        $.get("/api/Teretana", function (data, status) {
            data.sort((a, b) => {
                if (a.GodinaOtvaranja < b.GodinaOtvaranja) return 1;
                if (a.GodinaOtvaranja > b.GodinaOtvaranja) return -1;
                return 0;
            });
            let table = '<table>';
            table += '<tr><th>Naziv</th><th>Adresa</th><th>Godina otvaranja</th><th></th></tr>';
            for (element in data) {
                let teretana = '<td>' + data[element].Naziv + '</td>';
                teretana += '<td>' + data[element].Adresa.Ulica + ' ' + data[element].Adresa.Broj + ', ' + data[element].Adresa.Grad + '</td>';
                teretana += '<td>' + data[element].GodinaOtvaranja + '</td>';
                teretana += '<td><input type="submit" class="btn" value="Detalji"/></td>';
                table += '<tr> ' + teretana + '</tr>';
            }
            table += '</table>';
            $('#content').html(table);
        });
    });
    $("#searchByName").click(function () {
        let name = $("#name").val();
        if (name === "") {
            return loadGyms();
        }
        $.get('api/Teretana', { 'name': name }, function (result) {
            let table = '<table>';
            table += '<tr><th>Naziv</th><th>Adresa</th><th>Godina otvaranja</th><th></th></tr>';

            let teretana = '<td>' + result.Naziv + '</td>';
            teretana += '<td>' + result.Adresa.Ulica + ' ' + result.Adresa.Broj + ', ' + result.Adresa.Grad + '</td>';
            teretana += '<td>' + result.GodinaOtvaranja + '</td>';
            teretana += '<td><input type="submit" class="btn" value="Detalji"/></td>';
            table += '<tr> ' + teretana + '</tr>';

            table += '</table>';
            $('#content').html(table);
        });
    });

    $("#searchByAddress").click(function () {
        
        let ulica = $("#ulica").val();
        let broj = $("#broj").val();
        let grad = $("#grad").val();
        let pb = $("#postanskiBroj").val();

        if (ulica === "" && broj === "" && grad === "" && pb === "") {
            return loadGyms();
        }
        $.get('api/Teretana', { 'grad': grad, 'ulica': ulica, 'broj': broj, 'pb': pb, }, function (result) {
            let table = '<table>';
            table += '<tr><th>Naziv</th><th>Adresa</th><th>Godina otvaranja</th><th></th></tr>';

            let teretana = '<td>' + result.Naziv + '</td>';
            teretana += '<td>' + result.Adresa.Ulica + ' ' + result.Adresa.Broj + ', ' + result.Adresa.Grad + '</td>';
            teretana += '<td>' + result.GodinaOtvaranja + '</td>';
            teretana += '<td><input type="submit" class="btn" value="Detalji"/></td>';
            table += '<tr> ' + teretana + '</tr>';

            table += '</table>';
            $('#content').html(table);
        });
    });

    $("#searchByYear").click(function () {
        let from = $("#from").val();
        let to = $("#to").val();


        if (from === "" && to === "") {
            return loadGyms();
        }
        $.get('api/Teretana/', { 'from':from, 'to':to }, function (result) {
            let table = '<table>';
            table += '<tr><th>Naziv</th><th>Adresa</th><th>Godina otvaranja</th><th></th></tr>';
            for (element in result) {
                let teretana = '<td>' + result[element].Naziv + '</td>';
                teretana += '<td>' + result[element].Adresa.Ulica + ' ' + result[element].Adresa.Broj + ', ' + result[element].Adresa.Grad + '</td>';
                teretana += '<td>' + result[element].GodinaOtvaranja + '</td>';
                teretana += '<td><input type="submit" class="btn" value="Detalji"/></td>';
                table += '<tr> ' + teretana + '</tr>';
            }
            table += '</table>';
            $('#content').html(table);
        });
    });
}
);



function loadGyms() {
    $.get("/api/Teretana", function (data, status) {
        data.sort((a, b) => a.Naziv.localeCompare(b.Naziv))
        let table = '<table >';
        table += '<tr><th>Naziv</th><th>Adresa</th><th>Godina otvaranja</th><th></th></tr>';
        for (element in data) {
            let teretana = '<td>' + data[element].Naziv + '</td>';
            teretana += '<td>' + data[element].Adresa.Ulica + ' ' + data[element].Adresa.Broj + ', ' + data[element].Adresa.Grad + '</td>';
            teretana += '<td>' + data[element].GodinaOtvaranja + '</td>';
            teretana += '<td><input type="submit" class="btn" value="Detalji"/></td>';
            table += '<tr> ' + teretana + '</tr>';
        }
        table += '</table>';
        $('#content').html(table);
    });
}     






/*              let table = '<table>';
                table += '<tr><th>Naziv</th><th>Adresa</th><th>Godina otvaranja</th><th></th></tr>';

                let teretana = '<td>' + result[element].Naziv + '</td>';
                teretana += '<td>' + result[element].Adresa.Ulica + ' ' + result[element].Adresa.Broj + ', ' + result[element].Adresa.Grad + '</td>';
                teretana += '<td>' + result[element].GodinaOtvaranja + '</td>';
                teretana += '<td><input type="submit" class="btn" value="Detalji"/></td>';
                table += '<tr> ' + teretana + '</tr>';

                table += '</table>';
                $('#content').html(table);
*/