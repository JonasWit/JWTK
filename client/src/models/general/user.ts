class User {
    id: string;
    email: string;
    role: string;
    lastLogin: Date;
    claims: Claim[];
    accessKeys: AccessKey[];
    
    constructor(id: string, email: string, role: string, lastLogin: Date, claims: Claim[], accessKeys: AccessKey[]) {
     this.id = id;
     this.email = email;
     this.role = role;
     this.lastLogin = lastLogin;
     this.claims = claims;
     this.accessKeys =accessKeys;
    }
} 