using C_51125_ProyClaseQ4.Services;
using Microsoft.AspNetCore.Mvc;

namespace C_51125_ProyClaseQ4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FireBaseController : ControllerBase
    {
        private readonly FirebaseServices _firebaseServices;

        public FireBaseController(FirebaseServices firebaseServices)
        {
            _firebaseServices = firebaseServices;
        }

        [HttpGet("firebase")]
        public async Task<IActionResult> TestFireBase()
        {
            try
            {
                var db = _firebaseServices.GetFirestoreDb();

                //Intentar leer la coleccion de users
                var usersCollection = _firebaseServices.GetCollection("users");
                var snapshot = await usersCollection.Limit(1).GetSnapshotAsync();

                return Ok(new
                {
                    success = true,
                    message = "Conexion exitosa",
                    projectId = db.ProjectId,
                    documentsFounds = snapshot.Count
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Error al conectar con Firebase",
                    error = ex.Message,
                    stackTrace = ex.StackTrace
                });

            }
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Firebase Controller is working!");
        }
    }
}
