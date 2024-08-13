export interface StorageInterface {
  getAccessToken(): string | null;
  // getRefreshToken(): string;
  setAccessToken(token: string): void;
  // setRefreshToken(token: string): void;
  removeTokens(): void;
}
