var target : Transform;
var maxParticles : int = 15;
var rotationVelocity = 2;
private var particles : ParticleSystem.Particle[];

function Start () {
    target = transform.parent;
    particles = new ParticleSystem.Particle[maxParticles];
}

function Update () {

    GetComponent.<ParticleSystem>().startRotation = target.rotation.eulerAngles.y * 1 * Mathf.Deg2Rad;

    var count:int = GetComponent.<ParticleSystem>().GetParticles(particles);

    for(var i : int = 0; i <= count; i++)
    {
            particles[i].rotation = Mathf.LerpAngle(particles[i].rotation, target.rotation.eulerAngles.y * 1, Time.deltaTime * rotationVelocity);
            GetComponent.<ParticleSystem>().SetParticles(particles, count);
    }      
}