export interface AuthenticationResponse {
   email: string;
   token: string;
   tokenExpiresInMinutes: number;
   refreshToken: string;
   refreshTokenExpirationDateTimeUTC: string;
}