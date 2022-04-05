[HttpPost]
[Route("login")]
public IActionResult Login(Usuario usuario){
    if(usuario = usuario.Token){
        return Ok(entity);
    }
    return NotFound(usuario);
}
