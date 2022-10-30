#version 330 core
out vec4 FragColor;


struct Material {
    sampler2D diffuse;
    sampler2D specular;
    sampler2D emission;
    float shininess;
};
struct Light {
    vec3 position;
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
};

in vec3 Normal;
in vec3 FragPos;
in vec2 Tex;

  

uniform vec3 viewPos;
uniform Light light;
uniform Material material;
uniform float matrixLight;
uniform float matrixMove;
void main()
{
    //ambient
    vec3 ambient=texture(material.diffuse,Tex).rgb * light.ambient;
    
    //diffuse
    vec3 norm=normalize(Normal);
    vec3 lightDir=normalize(light.position-FragPos);
    float diff=max(dot(lightDir,norm),0.0f);
    vec3 diffuse= light.diffuse * diff * texture(material.diffuse,Tex).rgb ;

    
    //spec
    vec3 viewDir=normalize(viewPos-FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);
    float spec=pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    vec3 specular = light.specular * (spec * texture( material.specular,Tex).rgb);
    
    
    vec3 result=ambient+diffuse+specular+matrixLight * vec3(texture( material.emission,vec2(Tex.x,Tex.y+matrixMove)));
    
    FragColor = vec4(result, 1.0);
}
