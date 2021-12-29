class User {
    id: string;
    email: string;
    role: string;
    lastLogin: Date;
    
    constructor(id: string, email: string, role: string, lastLogin : Date) {
     this.id = id;
     this.email = email;
     this.role = role;
     this.lastLogin = lastLogin;
    }
} 