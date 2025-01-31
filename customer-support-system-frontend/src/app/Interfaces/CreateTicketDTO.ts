export interface CreateTicketDTO {
    Title: string;
    Description: string;
    CategoryId: number;
    CustomerSupportUserId: string;
    Status: string; // Assuming it's an enum in backend
    Priority: string; // Assuming it's an enum in backend
    Attachments: File[];
  }
  