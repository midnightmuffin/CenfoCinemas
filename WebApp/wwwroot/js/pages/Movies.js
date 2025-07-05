// JS que maneja todo el comportamiento de la página de movies
// Definir una clase JS, usando prototype

function MoviesViewController() {

    this.ViewName = "Movies";
    this.ApiEndPointName = "Movie";

    //Metodo constructor

    this.initView = function () {

        console.log("Movie init view --> Ok");
        // Llamar al método para llenar la tabla de peliculas
        this.LoadTable();

        // Asignar el evento click al botón de crear la pelicula
        $('#btnCreate').click(function () {
            // Llamar al método para crear una pelicula
            var vc = new MoviesViewController();
            vc.Create();
        });

        // Asignar el evento click al botón de editar la pelicula
        $('#btnUpdate').click(function () {
            // Llamar al método para actualizar una pelicula
            var vc = new MoviesViewController();
            vc.Update();
        });

        // Asignar el evento click al botón de eliminar la pelicula
        $('#btnDelete').click(function () {
            // Llamar al método para eliminar una pelicula
            var vc = new MoviesViewController();
            vc.Delete();
        });

        // Buscar por ID y llenar el formulario
        $('#btnSearchMovieId').click(function () {

            var id = $('#txtSearchMovieId').val();
            if (!id) { return; }

            // Obtenemos la data actual de la tabla
            var data = $('#tblMovies').DataTable().rows().data().toArray();

            var movieDTO = null;
            for (var i = 0; i < data.length; i++) {
                if (data[i].id == id) {
                    movieDTO = data[i];
                    break;
                }
            }

            if (movieDTO) {
                var vc = new MoviesViewController();
                vc.fillForm(movieDTO);
            } else {
                alert('Película no encontrada');
            }
        });

        // Disparar búsqueda con Enter
        $('#txtSearchMovieId').keyup(function (e) {
            if (e.key === 'Enter') $('#btnSearchMovieId').click();
        });
    };

    // Método para llenar la tabla de peliculas

    this.LoadTable = function () {

        // URL del servicio API para obtener las peliculas
        //https://localhost:7216/api/Movie/RetrieveAll

        var ca = new ControlActions();
        var service = this.ApiEndPointName + "/RetrieveAll"

        var urlService = ca.GetUrlApiService(service);

        /**
            {
                "title": "Coraline y la puerta secreta",
                "description": "Una niña descubre una puerta secreta en su nueva casa y entra a una realidad alterna que la refleja fielmente de muchas formas",
                "releaseDate": "2009-02-06T00:00:00",
                "genre": "Animación",
                "director": "Henry Selick",
                "id": 1,
                "created": "2025-06-14T20:13:50.13",
                "updated": "0001-01-01T00:00:00"
            }

                <tr>
                     <th>Title</th>
                     <th>Description</th>
                     <th>Release Date</th>
                     <th>Genre</th>
                     <th>Director</th>
                </tr>
         */

        var columns = [];
        columns[0] = { 'data': 'id' };
        columns[1] = { 'data': 'title' }
        columns[2] = { 'data': 'description' }
        columns[3] = { 'data': 'releaseDate' }
        columns[4] = { 'data': 'genre' }
        columns[5] = { 'data': 'director' }

        // Invocamos a DataTable para llenar la tabla de peliculas más robusta
        $('#tblMovies').DataTable({
            "ajax": {
                url: urlService,
                "dataSrc": ""
            },
            columns: columns
        });

        // Asignar eventos de carga de datos o binding según el click en la tabla

        $('#tblMovies tbody').on('click', 'tr', function () {
            //Extraemos la fila seleccionada
            var row = $(this).closest('tr');
            // Extraemos el DTO, esto nos devuelve el JSON de la pelicula seleccionado por la pelicula
            // Segun la data devuela por el API
            var movieDTO = $('#tblMovies').DataTable().row(row).data();
            // Binding con el form
            $('#txtId').val(movieDTO.id);
            $('#txtTitle').val(movieDTO.title);
            $('#txtDescription').val(movieDTO.description);
            $('#txtGenre').val(movieDTO.genre);
            $('#txtDirector').val(movieDTO.director);

            // La fecha tiene un formato
            var onlyDate = movieDTO.releaseDate.split('T');
            $('#txtRDate').val(onlyDate[0]);
        })
    };

    this.fillForm = function (m) {
        $('#txtId').val(m.id);
        $('#txtTitle').val(m.title);
        $('#txtDescription').val(m.description);
        $('#txtGenre').val(m.genre);
        $('#txtDirector').val(m.director);
        $('#txtRDate').val(m.releaseDate.split('T')[0]);
    };

    // Método para crear una nueva pelicula

    this.Create = function () {
        var movieDTO = {};
        // Atributos con valores default que son controlados por el API
        movieDTO.id = 0; // El API lo maneja como autoincremental
        movieDTO.created = "2025-01-01";
        movieDTO.updated = "2025-01-01";

        // Atributo que son capturados en pantalla
        movieDTO.title = $('#txtTitle').val();
        movieDTO.description = $('#txtDescription').val();
        movieDTO.genre = $('#txtGenre').val();
        movieDTO.director = $('#txtDirector').val();
        movieDTO.releaseDate = $('#txtRDate').val();

        // Enviar la data al API para crear la pelicula
        var ca = new ControlActions();
        var urlService = this.ApiEndPointName + "/Create";

        ca.PostToAPI(urlService, movieDTO, function () {
            // Recargar la tabla de peliculas
            $('#tblMovies').DataTable().ajax.reload();
        });
    }

    // Método para editar una pelicula existente (por implementar)
    this.Update = function () {

        var movieDTO = {};
        // Atributos con valores default que son controlados por el API
        movieDTO.id = $('#txtId').val();
        movieDTO.created = "2025-01-01";
        movieDTO.updated = "2025-01-01";

        // Atributo que son capturados en pantalla
        movieDTO.title = $('#txtTitle').val();
        movieDTO.description = $('#txtDescription').val();
        movieDTO.genre = $('#txtGenre').val();
        movieDTO.director = $('#txtDirector').val();
        movieDTO.releaseDate = $('#txtRDate').val();

        // Enviar la data al API para crear la pelicula
        var ca = new ControlActions();
        var urlService = this.ApiEndPointName + "/Update";

        ca.PutToAPI(urlService, movieDTO, function () {
            // Recargar la tabla de peliculas
            $('#tblMovies').DataTable().ajax.reload();
        });
    };

    this.Delete = function () {

        var movieDTO = {};
        // Atributos con valores default que son controlados por el API
        movieDTO.id = $('#txtId').val();
        movieDTO.created = "2025-01-01";
        movieDTO.updated = "2025-01-01";

        // Atributo que son capturados en pantalla
        movieDTO.title = $('#txtTitle').val();
        movieDTO.description = $('#txtDescription').val();
        movieDTO.genre = $('#txtGenre').val();
        movieDTO.director = $('#txtDirector').val();
        movieDTO.releaseDate = $('#txtRDate').val();

        // Enviar la data al API para crear la pelicula
        var ca = new ControlActions();
        var urlService = this.ApiEndPointName + "/Delete";

        ca.DeleteToAPI(urlService, movieDTO, function () {
            // Recargar la tabla de peliculas
            $('#tblMovies').DataTable().ajax.reload();
        });
    };
}

$(document).ready(function () {

    // Crear una instancia de la clase MoviesViewController y llamar al método initView
    var vc = new MoviesViewController();
    vc.initView();

})