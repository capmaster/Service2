node
{
    checkout scm

    docker.withRegistry('https://registry.hub.docker.com','dockerhubcreds')
    {
        def img = docker.build("icecoldrax/service2:${env.BUILD_ID}");
        img.push();
    }
}