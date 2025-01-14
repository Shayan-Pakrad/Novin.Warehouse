export interface LoginResponseDto {
    tokenType: string;
    accessToken: string;
    expiresIn: number;
    refreshToken: string;
}