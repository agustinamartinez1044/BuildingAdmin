export interface LoginModel {
  token: string;
}

export interface UserModel {
  name: string;
  email: string;
  role: string;
}

export interface InvitationModel {
  name: string;
  email: string;
  role: number;
  expirationDate: string;
}

export interface Category {
  name: string;
  id: number;
  relatedTo: string | null;
}

export interface AssignedTo {
  lastName: string;
  buildings: any | null;
  id: number;
  name: string;
  email: string;
  password: string;
}

export interface TicketModel {
  id: number;
  description: string;
  creationDate: string;
  apartment: string | null;
  totalCost: number;
  createdBy: string | null;
  category: Category;
  status: string;
  attentionDate: string;
  closingDate: string;
  assignedTo: AssignedTo;
}
export interface EditInvitationModel {
  id: number;
  expirationDate: string;
}

export interface TicketsByCategories {
  tickets: Object[];
}

export interface Categories {
  categories: Object[];
}

export interface CreateCategoryModel {
  name: string;
}

export interface BuildingModel {
  buildings: Object[];
}

export interface AdminModel {
  name: string;
  lastName: string;
  email: string;
  password: string;
}

export interface AcceptInvitationModel {
  email: string;
  password: string;
}

export interface RejectInvitationModel {
  email: string;
}

export interface AcceptInvitationOutputModel{
  userId: number;
}