let settings = {
  spotlightRadius: 400,
  boxCount: 50,
  moveSpeed: 1,
  turboSpeed: 5,
}

function state(s){
  if (s==="init"){
    const app = new Application;
    let wrapper = document.querySelector('#game-wrapper');
    wrapper.addEventListener('mousemove',function(e){
      app.universe.light.position.x = e.clientX*window.devicePixelRatio;
      app.universe.light.position.y = e.clientY*window.devicePixelRatio;
    });
    wrapper.addEventListener('mousedown',() => {
      app.universe.speed = settings.turboSpeed;
    });
    wrapper.addEventListener('mouseup',()=>{
      app.universe.speed = settings.moveSpeed;
    });
  }
}
class Application {
  constructor(){
    this.width = null;
    this.height = null;
    let wrapper = document.querySelector('#game-wrapper');
    this.canvas = document.createElement('canvas');
    wrapper.appendChild(this.canvas);
    this.context = this.canvas.getContext('2d');
    this.canvasOverlay = document.createElement('canvas');
    wrapper.appendChild(this.canvasOverlay);
    this.contextOverlay = this.canvasOverlay.getContext('2d');
    this.resize();
    window.addEventListener('resize', () => this.resize(), false);
    this.universe = new Universe(this.width,this.height);
    this.render();
  }
  render(){
    this.context.clearRect(0,0,this.width*window.devicePixelRatio,this.height*window.devicePixelRatio);
    let gradient=this.context.createRadialGradient(this.universe.light.position.x,this.universe.light.position.y,0.9*settings.spotlightRadius,this.universe.light.position.x,this.universe.light.position.y,0);
      gradient.addColorStop(0,"#CAA669");
    gradient.addColorStop(1,"#fff");
    this.context.fillStyle = gradient;
    this.context.fillRect(0,0,this.width,this.height);

  
    let boxes = this.universe.boxArray;
    
    for (var i=0; i<boxes.length;i++){
    
      let box = boxes[i];

     
      this.context.fillStyle = box.color;
      this.context.strokeStyle = box.color;
     
      for (let i = 0;i<box.shadowCorners.length;i++){
        let j = (i<box.shadowCorners.length-1) ? i+1:0
        this.context.beginPath();
        this.context.moveTo(box.corners[i].x,box.corners[i].y);
        this.context.lineTo(box.shadowCorners[i].x,box.shadowCorners[i].y);
        this.context.lineTo(box.shadowCorners[j].x,box.shadowCorners[j].y);
        this.context.lineTo(box.corners[j].x,box.corners[j].y);
        this.context.closePath();
        this.context.stroke();
        this.context.fill();
      }

      this.context.beginPath();
      this.context.moveTo(box.corners[0].x,box.corners[0].y);
      for (let i = 1;i<box.sides;i++){
        this.context.lineTo(box.corners[i].x,box.corners[i].y);
      }
      this.context.closePath();
      this.context.fill();
      this.context.stroke();
    }

    this.context.beginPath();
    this.context.arc(this.universe.light.position.x,this.universe.light.position.y,2,0,2*Math.PI);
    this.context.stroke();
    this.contextOverlay.clearRect(0,0,this.width*window.devicePixelRatio,this.height*window.devicePixelRatio);

    gradient=this.contextOverlay.createRadialGradient(this.universe.light.position.x,this.universe.light.position.y,settings.spotlightRadius,this.universe.light.position.x,this.universe.light.position.y,0);
    gradient.addColorStop(0,"#0a0e2377");
    gradient.addColorStop(0.5,"transparent");

    this.contextOverlay.fillStyle = gradient;
    this.contextOverlay.fillRect(0,0,this.width,this.height);
    this.update();
    window.requestAnimationFrame(()=>this.render());
  }
  update(){
    this.universe.update(this.center);
  }
  resize(){
    this.width = window.innerWidth;
    this.height = window.innerHeight / 1.5;
    document.querySelector('#game-wrapper').style.width = this.width+'px';
    document.querySelector('#game-wrapper').style.height = this.height+'px';
    this.width *= window.devicePixelRatio;
    this.height *= window.devicePixelRatio;

    this.canvas.width = this.width;
    this.canvas.height = this.height;
    this.canvasOverlay.width = this.width;
    this.canvasOverlay.height = this.height;

    if (this.universe) this.universe.generateBoxes();
    this.context.globalCompositeOperation = 'screen';
    this.center = {
      x: this.width/2,
      y: this.height/2
    }
  }
}
class Universe{
  constructor(width,height){
    this.width = width;
    this.height = height;
    this.speed = settings.moveSpeed;
    this.boxArray = [];
    this.generateBoxes();
    this.light = new Light(width*.75,height*.35);
  }

  generateBoxes(){
    this.boxArray = [];
    for (let i=0;i<settings.boxCount;i++){
      let box = new Box(this.width, this.height);
      this.boxArray.push(box);
    }
  }
  update(centerPixel){
    let dx = (centerPixel.x - this.light.position.x)/100;
    let dy = (centerPixel.y - this.light.position.y)/100;
    Array.prototype.forEach.call(this.boxArray, (box)=>{
      box.update(centerPixel,dx*this.speed,dy*this.speed,this.light);
    });
  }
}

class Box {
  constructor(width,height){
    this.position = {
      x: Math.random()*width,
      y: Math.random()*height,
    }
    this.radius = Math.random()*30;
    this.rotation = Math.random();
    this.rotationSpeed = (Math.random()-0.5)/100;
    this.sides = Math.floor(Math.random()*6+3);
    this.color = this.getRandomColor();
    this.corners = [];
    this.getCornerLocations();
    this.shadowCorners = this.corners;
  }

  update(centerPixel,dx,dy,light){
    this.position.x += dx;
    this.position.y += dy;
    if (this.position.x<-this.radius){
      this.position.x+=centerPixel.x*2+this.radius*2;
    } else if (this.position.x>centerPixel.x*2+this.radius){
      this.position.x -= centerPixel.x*2+this.radius*2;
    }
    if (this.position.y<0-this.radius){
      this.position.y+=centerPixel.y*2+this.radius*2;
    } else if (this.position.y>centerPixel.y*2+this.radius){
      this.position.y-=centerPixel.y*2+this.radius*2;
    }
    this.rotation += this.rotationSpeed;
    this.corners = [];
    this.shadowCorners = [];
    this.getCornerLocations();
    this.getShadowCorners(light);
  }
  getCornerLocations(){   
    let internalAngle = Math.PI*2/this.sides;
    for (var i=0;i<this.sides;i++){
      let x = this.position.x + this.radius*Math.sin(this.rotation+i*internalAngle);
      let y = this.position.y + this.radius*Math.cos(this.rotation+i*internalAngle);
      this.corners.push({x:x,y:y});
    }
  }
  getShadowCorners(light){
    for (var i=0;i<this.sides;i++){
      let dx = this.corners[i].x - light.position.x;
      let dy = this.corners[i].y - light.position.y;
      let dist = Math.sqrt(dx*dx+dy*dy);
      let x = light.position.x + dx * settings.spotlightRadius / dist * 20;  // 20 times the distance of our light radius
      let y = light.position.y + dy * settings.spotlightRadius / dist * 20; 
      this.shadowCorners.push({x:x,y:y});
    }
  }
  getRandomColor() {
    let letters = '0123456789ABCDEF';
    let color = '#';
    for (let i = 0; i < 6; i++ ) {
      color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
  }
}
class Light {
  constructor(x,y){
    this.position = {
      x: x,
      y: y,
    }
  }
}
window.onload = function() {
  state("init");
}